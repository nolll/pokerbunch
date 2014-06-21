using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
	public class InvitePlayerConfirmationPageModel : PageModel
    {
	    public InvitePlayerConfirmationPageModel(BunchContextResult contextResult)
            : base("Player Invited", contextResult)
	    {
	    }
    }
}