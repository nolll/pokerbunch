using System.Collections.Generic;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Invite;

namespace Web.Controllers
{
    public class InvitePlayerController : BaseController
    {
        [Authorize]
        [Route(InvitePlayerUrl.Route)]
        public ActionResult Invite(string playerId)
        {
            return ShowForm(playerId);
        }

        [HttpPost]
        [Authorize]
        [Route(InvitePlayerUrl.Route)]
        public ActionResult Invite_Post(string playerId, InvitePlayerPostModel postModel)
        {
            var errors = new List<string>();

            try
            {
                var request = new InvitePlayer.Request(playerId, postModel.Email);
                var result = UseCase.InvitePlayer.Execute(request);
                return Redirect(new InvitePlayerConfirmationUrl(result.PlayerId).Relative);
            }
            catch (ValidationException ex)
            {
                errors.AddRange(ex.Messages);
            }

            return ShowForm(playerId, postModel, errors);
        }

        [Route(InvitePlayerConfirmationUrl.Route)]
        public ActionResult Invited(string playerId)
        {
            var invitePlayerConfirmation = UseCase.InvitePlayerConfirmation.Execute(new InvitePlayerConfirmation.Request(playerId));
            var contextResult = GetBunchContext(invitePlayerConfirmation.Slug);
            var model = new InvitePlayerConfirmationPageModel(contextResult);
            return View(model);
        }

        private ActionResult ShowForm(string playerId, InvitePlayerPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var invitePlayerForm = UseCase.InvitePlayerForm.Execute(new InvitePlayerForm.Request(playerId));
            var context = GetBunchContext(invitePlayerForm.Slug);
            var model = new InvitePlayerPageModel(context, postModel, errors);
            return View(model);
        }
    }
}