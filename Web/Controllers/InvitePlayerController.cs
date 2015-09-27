using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Invite;

namespace Web.Controllers
{
    public class InvitePlayerController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.PlayerInvite)]
        public ActionResult Invite(int id)
        {
            return ShowForm(id);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.PlayerInvite)]
        public ActionResult Invite_Post(int id, InvitePlayerPostModel postModel)
        {
            try
            {
                var request = new InvitePlayer.Request(CurrentUserName, id, postModel.Email, new AddUserUrl().Absolute, new JoinBunchUrl("{0}").Absolute);
                var result = UseCase.InvitePlayer.Execute(request);
                return Redirect(new InvitePlayerConfirmationUrl(result.PlayerId).Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(id, postModel);
        }

        [Route(WebRoutes.PlayerInviteConfirmation)]
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