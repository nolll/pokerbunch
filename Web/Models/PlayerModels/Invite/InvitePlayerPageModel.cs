using Core.UseCases.BunchContext;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerPageModel : BunchPageModel
    {
        public string Email { get; private set; }

        public InvitePlayerPageModel(BunchContextResult contextResult, InvitePlayerPostModel postModel)
            : base("Invite Player", contextResult)
        {
            if (postModel == null) return;
            Email = postModel.Email;
        }
    }
}