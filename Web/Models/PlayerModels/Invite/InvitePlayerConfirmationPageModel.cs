using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerConfirmationPageModel : BunchPageModel
    {
	    public InvitePlayerConfirmationPageModel(BunchContextResult contextResult)
            : base("Player Invited", contextResult)
	    {
	    }
    }
}