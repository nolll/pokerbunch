using System.Collections.Generic;
using Core.Exceptions;
using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.PlayerModels.Invite;

namespace Web.Controllers
{
    public class InvitePlayerController : BunchController
    {
        private readonly InvitePlayer _invitePlayer;
        private readonly InvitePlayerConfirmation _invitePlayerConfirmation;
        private readonly InvitePlayerForm _invitePlayerForm;

        public InvitePlayerController(
            AppSettings appSettings, 
            CoreContext coreContext,
            BunchContext bunchContext,
            InvitePlayer invitePlayer, 
            InvitePlayerConfirmation invitePlayerConfirmation,
            InvitePlayerForm invitePlayerForm) 
            : base(
                appSettings,
                coreContext,
                bunchContext)
        {
            _invitePlayer = invitePlayer;
            _invitePlayerConfirmation = invitePlayerConfirmation;
            _invitePlayerForm = invitePlayerForm;
        }

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
                var result = _invitePlayer.Execute(request);
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
            var invitePlayerConfirmation = _invitePlayerConfirmation.Execute(new InvitePlayerConfirmation.Request(playerId));
            var contextResult = GetBunchContext(invitePlayerConfirmation.Slug);
            var model = new InvitePlayerConfirmationPageModel(AppSettings, contextResult);
            return View(model);
        }

        private ActionResult ShowForm(string playerId, InvitePlayerPostModel postModel = null, IEnumerable<string> errors = null)
        {
            var invitePlayerForm = _invitePlayerForm.Execute(new InvitePlayerForm.Request(playerId));
            var context = GetBunchContext(invitePlayerForm.Slug);
            var model = new InvitePlayerPageModel(AppSettings, context, postModel, errors);
            return View(model);
        }
    }
}