using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using Web.Common.Routes;
using Web.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Extensions;
using Web.Models.PlayerModels.Invite;

namespace Web.Controllers
{
    public class InvitePlayerController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Player.Invite)]
        public ActionResult Invite(string id)
        {
            return ShowForm(id);
        }

        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Player.Invite)]
        public ActionResult Invite_Post(string id, InvitePlayerPostModel postModel)
        {
            try
            {
                var request = new InvitePlayer.Request(Identity.UserName, id, postModel.Email, new AddUserUrl().GetAbsolute(), new JoinBunchUrl("{0}").GetAbsolute(), new JoinBunchUrl("{0}", "{1}").GetAbsolute());
                var result = UseCase.InvitePlayer.Execute(request);
                return Redirect(new InvitePlayerConfirmationUrl(result.PlayerId).Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }

            return ShowForm(id, postModel);
        }

        [Route(WebRoutes.Player.InviteConfirmation)]
        public ActionResult Invited(string id)
        {
            var invitePlayerConfirmation = UseCase.InvitePlayerConfirmation.Execute(new InvitePlayerConfirmation.Request(Identity.UserName, id));
            var contextResult = GetBunchContext(invitePlayerConfirmation.Slug);
            var model = new InvitePlayerConfirmationPageModel(contextResult);
            return View("~/Views/Pages/InvitePlayer/InviteConfirmation.cshtml", model);
        }

        private ActionResult ShowForm(string id, InvitePlayerPostModel postModel = null)
        {
            var invitePlayerForm = UseCase.InvitePlayerForm.Execute(new InvitePlayerForm.Request(Identity.UserName, id));
            var context = GetBunchContext(invitePlayerForm.Slug);
            var model = new InvitePlayerPageModel(context, postModel);
            return View("~/Views/Pages/InvitePlayer/Invite.cshtml", model);
        }
    }
}