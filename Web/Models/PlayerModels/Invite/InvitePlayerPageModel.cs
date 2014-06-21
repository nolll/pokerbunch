using Application.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerPageModel : PageModel
    {
        public string Email { get; set; }

        public InvitePlayerPageModel(BunchContextResult contextResult, InvitePlayerPostModel postModel)
            : base("Invite Player", contextResult)
        {
            if (postModel == null) return;
            Email = postModel.Email;
        }
    }
}