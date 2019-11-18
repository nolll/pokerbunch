using System.Collections.Generic;
using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.ErrorModels;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerPageModel : BunchPageModel
    {
        public string Email { get; }
        public ErrorListModel Errors { get; }

        public InvitePlayerPageModel(AppSettings appSettings, BunchContext.Result contextResult, InvitePlayerPostModel postModel, IEnumerable<string> errors)
            : base(appSettings, contextResult)
        {
            if (postModel == null) return;
            Email = postModel.Email;
            Errors = new ErrorListModel(errors);
        }

        public override string BrowserTitle => "Invite Player";

        public override View GetView()
        {
            return new View("~/Views/Pages/InvitePlayer/Invite.cshtml");
        }
    }
}