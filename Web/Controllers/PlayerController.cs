using System.Web.Mvc;
using Application.Urls;
using Application.UseCases.BunchContext;
using Application.UseCases.InvitePlayer;
using Application.UseCases.PlayerBadges;
using Application.UseCases.PlayerDetails;
using Application.UseCases.PlayerFacts;
using Application.UseCases.PlayerList;
using Web.Commands.PlayerCommands;
using Web.Models.PlayerModels.Add;
using Web.Models.PlayerModels.Details;
using Web.Models.PlayerModels.Invite;
using Web.Models.PlayerModels.List;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class PlayerController : ControllerBase
    {
        private readonly IBunchContextInteractor _bunchContextInteractor;
        private readonly IPlayerDetailsInteractor _playerDetailsInteractor;
        private readonly IPlayerFactsInteractor _playerFactsInteractor;
        private readonly IPlayerBadgesInteractor _playerBadgesInteractor;
        private readonly IPlayerListInteractor _playerListInteractor;
        private readonly IInvitePlayerInteractor _invitePlayerInteractor;
        private readonly IPlayerCommandProvider _playerCommandProvider;

        public PlayerController(
            IBunchContextInteractor bunchContextInteractor,
            IPlayerDetailsInteractor playerDetailsInteractor,
            IPlayerFactsInteractor playerFactsInteractor,
            IPlayerBadgesInteractor playerBadgesInteractor,
            IPlayerListInteractor playerListInteractor,
            IInvitePlayerInteractor invitePlayerInteractor,
            IPlayerCommandProvider playerCommandProvider)
	    {
            _bunchContextInteractor = bunchContextInteractor;
            _playerDetailsInteractor = playerDetailsInteractor;
            _playerFactsInteractor = playerFactsInteractor;
            _playerBadgesInteractor = playerBadgesInteractor;
            _playerListInteractor = playerListInteractor;
            _invitePlayerInteractor = invitePlayerInteractor;
            _playerCommandProvider = playerCommandProvider;
	    }

        [AuthorizePlayer]
	    public ActionResult Index(string slug)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
            var playerListResult = _playerListInteractor.Execute(new PlayerListRequest(slug));
            var model = new PlayerListPageModel(contextResult, playerListResult);
			return View("List", model);
		}

        [AuthorizePlayer]
        public ActionResult Details(string slug, int playerId)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
            var detailsResult = _playerDetailsInteractor.Execute(new PlayerDetailsRequest(slug, playerId));
            var factsResult = _playerFactsInteractor.Execute(new PlayerFactsRequest(slug, playerId));
            var badgesResult = _playerBadgesInteractor.Execute(new PlayerBadgesRequest(slug, playerId));
            var model = new PlayerDetailsPageModel(contextResult, detailsResult, factsResult, badgesResult);
			return View("Details", model);
		}

        [AuthorizeManager]
        public ActionResult Add(string slug)
        {
            var model = BuildAddModel(slug);
            return View("Add", model);
		}

        [HttpPost]
        [AuthorizeManager]
        public ActionResult Add(string slug, AddPlayerPostModel postModel)
        {
            var command = _playerCommandProvider.GetAddCommand(slug, postModel);
            if (command.Execute())
            {
                return Redirect(new AddPlayerConfirmationUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildAddModel(slug, postModel);
			return View("Add", model);
		}

        private AddPlayerPageModel BuildAddModel(string slug, AddPlayerPostModel postModel = null)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
            return new AddPlayerPageModel(contextResult, postModel);
        }

        public ActionResult Created(string slug)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
            var model = new AddPlayerConfirmationPageModel(contextResult);
			return View("AddConfirmation", model);
		}

        [AuthorizeManager]
        public ActionResult Delete(string slug, int playerId)
        {
            var command = _playerCommandProvider.GetDeleteCommand(slug, playerId);
            if (command.Execute())
            {
                return Redirect(new PlayerIndexUrl(slug).Relative);
            }
            return Redirect(new PlayerDetailsUrl(slug, playerId).Relative);
		}

        [AuthorizeManager]
        public ActionResult Invite(string slug, int playerId)
        {
            var model = BuildInviteModel(slug);
            return View("Invite", model);
		}

        [HttpPost]
        [AuthorizeManager]
        public ActionResult Invite(string slug, int playerId, InvitePlayerPostModel postModel)
        {
            var request = new InvitePlayerRequest(slug, playerId, postModel.Email);
            var result = _invitePlayerInteractor.Execute(request);

            if (result.Success)
                return Redirect(result.ReturnUrl.Relative);

            AddModelErrors(result.Errors);
            var model = BuildInviteModel(slug, postModel);
            return View("Invite", model);
		}

        private InvitePlayerPageModel BuildInviteModel(string slug, InvitePlayerPostModel postModel = null)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
            return new InvitePlayerPageModel(contextResult, postModel);
        }

        public ActionResult Invited(string slug, int playerId)
        {
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest(slug));
            var model = new InvitePlayerConfirmationPageModel(contextResult);
			return View("InviteConfirmation", model);
		}
    }

}