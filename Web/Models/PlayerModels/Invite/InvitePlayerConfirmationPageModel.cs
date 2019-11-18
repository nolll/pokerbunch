using Core.Settings;
using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerConfirmationPageModel : BunchPageModel
    {
	    public InvitePlayerConfirmationPageModel(AppSettings appSettings, BunchContext.Result contextResult)
            : base(appSettings, contextResult)
	    {
	    }

        public override string BrowserTitle => "Player Invited";

        public override View GetView()
        {
            return new View("~/Views/Pages/InvitePlayer/InviteConfirmation.cshtml");
        }
    }
}