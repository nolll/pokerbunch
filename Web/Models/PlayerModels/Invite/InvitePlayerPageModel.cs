using Core.UseCases;
using Web.Extensions;
using Web.Models.PageBaseModels;

namespace Web.Models.PlayerModels.Invite
{
    public class InvitePlayerPageModel : BunchPageModel
    {
        public string Email { get; private set; }

        public InvitePlayerPageModel(BunchContext.Result contextResult, InvitePlayerPostModel postModel)
            : base(contextResult)
        {
            if (postModel == null) return;
            Email = postModel.Email;
        }

        public override string BrowserTitle => "Invite Player";

        public override View GetView()
        {
            return new View("~/Views/Pages/InvitePlayer/Invite.cshtml");
        }
    }
}