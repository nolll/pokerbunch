using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases.InvitePlayer;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Invite;
using Web.Urls;

namespace Web.Controllers
{
    public class InvitePlayerController : BaseController
    {
        [Authorize]
        [Route("{slug}/player/invite/{playerId:int}")]
        public ActionResult Invite(string slug, int playerId)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route("{slug}/player/invite/{playerId:int}")]
        public ActionResult Invite_Post(string slug, int playerId, InvitePlayerPostModel postModel)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            var request = new InvitePlayerRequest(slug, playerId, postModel.Email, new AddUserUrl().Absolute);

            try
            {
                var result = UseCase.InvitePlayer.Execute(request);
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
            var contextResult = GetBunchContext(slug);
            var model = new InvitePlayerConfirmationPageModel(contextResult);
            return View("~/Views/Pages/InvitePlayer/InviteConfirmation.cshtml", model);
        }

        private ActionResult ShowForm(string slug, InvitePlayerPostModel postModel = null)
        {
            var contextResult = GetBunchContext(slug);
            var model = new InvitePlayerPageModel(contextResult, postModel);
            return View("~/Views/Pages/InvitePlayer/Invite.cshtml", model);
        }
    }
}