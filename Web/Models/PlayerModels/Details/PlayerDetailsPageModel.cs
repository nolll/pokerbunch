using Application.UseCases.BunchContext;
using Application.UseCases.PlayerDetails;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Badges;
using Web.Models.PlayerModels.Facts;

namespace Web.Models.PlayerModels.Details
{
    public class PlayerDetailsPageModel : PageModel
    {
        public string DisplayName { get; private set; }
        public string DeleteUrl { get; private set; }
        public bool ShowUserInfo { get; private set; }
        public bool DeleteEnabled { get; private set; }
        public string UserUrl { get; private set; }
        public string InvitationUrl { get; private set; }
        public AvatarModel AvatarModel { get; private set; }
        public PlayerFactsModel PlayerFactsModel { get; set; }
        public PlayerBadgesModel PlayerBadgesModel { get; set; }

        public PlayerDetailsPageModel(BunchContextResult contextResult, PlayerDetailsResult playerDetailsResult)
            : base("Player Details", contextResult)
        {
            DisplayName = playerDetailsResult.DisplayName;
            DeleteUrl = playerDetailsResult.DeleteUrl.Relative;
            DeleteEnabled = playerDetailsResult.CanDelete;
            ShowUserInfo = playerDetailsResult.IsUser;
            UserUrl = playerDetailsResult.UserUrl.Relative;
            AvatarModel = new AvatarModel(playerDetailsResult.AvatarUrl);
            InvitationUrl = playerDetailsResult.InvitationUrl.Relative;
        }
    }
}