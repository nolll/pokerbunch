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
        public ActionResult Invite(int id)
        {
            return ShowForm(id);
        }

        [HttpPost]
        [Authorize]
        [Route(Routes.PlayerInvite)]
        public ActionResult Invite_Post(int id, InvitePlayerPostModel postModel)
        {
            try
            {
                var request = new InvitePlayer.Request(CurrentUserName, id, postModel.Email, new AddUserUrl().Absolute);
                var result = UseCase.InvitePlayer.Execute(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(id, postModel);
        }

        [Route(Routes.PlayerInviteConfirmation)]
        public ActionResult Invited(int id)
        {
            var invitePlayerConfirmation = UseCase.InvitePlayerConfirmation.Execute(new InvitePlayerConfirmation.Request(CurrentUserName, id));
            var contextResult = GetBunchContext(invitePlayerConfirmation.Slug);
            var model = new InvitePlayerConfirmationPageModel(contextResult);
            return View("~/Views/Pages/InvitePlayer/InviteConfirmation.cshtml", model);
        }

        private ActionResult ShowForm(int id, InvitePlayerPostModel postModel = null)
        {
            var invitePlayerForm = UseCase.InvitePlayerForm.Execute(new InvitePlayerForm.Request(CurrentUserName, id));
            var context = GetBunchContext(invitePlayerForm.Slug);
            var model = new InvitePlayerPageModel(context, postModel);
            return View("~/Views/Pages/InvitePlayer/Invite.cshtml", model);
        }
    }
}