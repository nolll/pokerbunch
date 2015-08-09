using Core.UseCases;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerPageModel : BunchPageModel
    {
        public string Email { get; private set; }

        public InvitePlayerPageModel(BunchContext.Result contextResult, InvitePlayerPostModel postModel)
            : base("Invite Player", contextResult)
        {
            if (postModel == null) return;
            Email = postModel.Email;
        }
    }
}