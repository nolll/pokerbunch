using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Invite;
using Web.Urls;

namespace Web.Controllers
{
    public class InvitePlayerController : BaseController
    {
        [Authorize]
        [Route(Routes.PlayerInvite)]
        public ActionResult Invite(string slug, int playerId)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [Authorize]
        [Route(Routes.PlayerInvite)]
        public ActionResult Invite_Post(string slug, int playerId, InvitePlayerPostModel postModel)
        {
            var request = new InvitePlayer.Request(CurrentUserName, slug, playerId, postModel.Email, new AddUserUrl().Absolute);

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

        [Route(Routes.PlayerInviteConfirmation)]
        public ActionResult Invited(string slug, int playerId)
        {
            var contextResult = GetBunchContextBySlug(slug);
            var model = new InvitePlayerConfirmationPageModel(contextResult);
            return View("~/Views/Pages/InvitePlayer/InviteConfirmation.cshtml", model);
        }

        private ActionResult ShowForm(string slug, InvitePlayerPostModel postModel = null)
        {
            var context = GetBunchContextBySlug(slug);
            var model = new InvitePlayerPageModel(context, postModel);
            return View("~/Views/Pages/InvitePlayer/Invite.cshtml", model);
        }
    }
}