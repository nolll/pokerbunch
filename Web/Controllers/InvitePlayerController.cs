using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.BunchContext;
using Core.UseCases.InvitePlayer;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Invite;

namespace Web.Controllers
{
    public class InvitePlayerController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/player/invite/{playerId:int}")]
        public ActionResult Invite(string slug, int playerId)
        {
            RequireManager(slug);
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route("{slug}/player/invite/{playerId:int}")]
        public ActionResult Invite_Post(string slug, int playerId, InvitePlayerPostModel postModel)
        {
            RequireManager(slug);
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

            return ShowForm(slug, postModel);
        }

        [Route("{slug}/player/invited/{playerId:int}")]
        public ActionResult Invited(string slug, int playerId)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var model = new InvitePlayerConfirmationPageModel(contextResult);
            return View("~/Views/Pages/InvitePlayer/InviteConfirmation.cshtml", model);
        }

        private ActionResult ShowForm(string slug, InvitePlayerPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var model = new InvitePlayerPageModel(contextResult, postModel);
            return View("~/Views/Pages/InvitePlayer/Invite.cshtml", model);
        }
    }
}