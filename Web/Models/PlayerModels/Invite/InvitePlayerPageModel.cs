using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerPageModel : PageModel
    {
        public string Email { get; set; }

        public InvitePlayerPageModel(BunchContextResult bunchContextResult, InvitePlayerPostModel postModel)
            : base("Invite Player", bunchContextResult)
        {
            if (postModel == null) return;
            Email = postModel.Email;
        }
    }
}