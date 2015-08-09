using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerConfirmationPageModel : BunchPageModel
    {
	    public InvitePlayerConfirmationPageModel(BunchContext.Result contextResult)
            : base("Player Invited", contextResult)
	    {
	    }
    }
}