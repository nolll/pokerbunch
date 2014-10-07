using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.AddPlayer;
using Core.UseCases.BunchContext;
using Core.UseCases.DeletePlayer;
using Core.UseCases.InvitePlayer;
using Core.UseCases.PlayerBadges;
using Core.UseCases.PlayerDetails;
using Core.UseCases.PlayerFacts;
using Core.UseCases.PlayerList;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Details;
using Web.Models.PlayerModels.Invite;
using Web.Models.PlayerModels.List;
using Web.Security.Attributes;
using ControllerBase = Web.Controllers.Base.ControllerBase;

namespace Web.Controllers
{
    public class PlayerController : ControllerBase
    {
        [AuthorizePlayer]
        [Route("{slug}/player/index")]
	    public ActionResult Index(string slug)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var playerListResult = UseCase.PlayerList(new PlayerListRequest(slug));
            var model = new PlayerListPageModel(contextResult, playerListResult);
			return View("List", model);
		}

        [AuthorizePlayer]
        [Route("{slug}/player/details/{playerId:int}")]
        public ActionResult Details(string slug, int playerId)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var detailsResult = UseCase.PlayerDetails(new PlayerDetailsRequest(slug, playerId));
            var factsResult = UseCase.PlayerFacts(new PlayerFactsRequest(slug, playerId));
            var badgesResult = UseCase.PlayerBadges(new PlayerBadgesRequest(slug, playerId));
            var model = new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
			return View("Details", model);
		}

        [AuthorizeManager]
        [Route("{slug}/player/add")]
        public ActionResult Add(string slug)
        {
            var model = BuildAddModel(slug);
            return View("Add", model);
		}

        [HttpPost]
        [AuthorizeManager]
        [Route("{slug}/player/add")]
        public ActionResult Add_Post(string slug, AddPlayerPostModel postModel)
        {
            var request = new AddPlayerRequest(slug, postModel.Name);

            try
            {
                var result = UseCase.AddPlayer(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            catch (PlayerExistsException ex)
            {
                AddModelError(ex.Message);
            }

            var model = BuildAddModel(slug, postModel);
            return View("Add", model);
		}

        [Route("{slug}/player/created")]
        public ActionResult Created(string slug)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var model = new AddPlayerConfirmationPageModel(contextResult);
			return View("AddConfirmation", model);
		}

        [AuthorizeManager]
        [Route("{slug}/player/delete/{playerId:int}")]
        public ActionResult Delete(string slug, int playerId)
        {
            var request = new DeletePlayerRequest(slug, playerId);
            var result = UseCase.DeletePlayer(request);
            return Redirect(result.ReturnUrl.Relative);
		}

        [AuthorizeManager]
        [Route("{slug}/player/invite/{playerId:int}")]
        public ActionResult Invite(string slug, int playerId)
        {
            var model = BuildInviteModel(slug);
            return View("Invite", model);
		}

        [HttpPost]
        [AuthorizeManager]
        [Route("{slug}/player/invite/{playerId:int}")]
        public ActionResult Invite_Post(string slug, int playerId, InvitePlayerPostModel postModel)
        {
            var request = new InvitePlayerRequest(slug, playerId, postModel.Email);

            try
            {
                var result = UseCase.InvitePlayer(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            var model = BuildInviteModel(slug, postModel);
            return View("Invite", model);
		}

        [Route("{slug}/player/invited/{playerId:int}")]
        public ActionResult Invited(string slug, int playerId)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var model = new InvitePlayerConfirmationPageModel(contextResult);
			return View("InviteConfirmation", model);
		}

        private AddPlayerPageModel BuildAddModel(string slug, AddPlayerPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            return new AddPlayerPageModel(contextResult, postModel);
        }

        private InvitePlayerPageModel BuildInviteModel(string slug, InvitePlayerPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            return new InvitePlayerPageModel(contextResult, postModel);
        }
    }

}