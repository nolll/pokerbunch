using Application.UseCases.BunchContext;
using Application.UseCases.PlayerBadges;
using Application.UseCases.PlayerDetails;
using Application.UseCases.PlayerFacts;
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
        public PlayerFactsModel PlayerFactsModel { get; private set; }
        public PlayerBadgesModel PlayerBadgesModel { get; private set; }

        public PlayerDetailsPageModel(BunchContextResult contextResult, PlayerDetailsResult detailsResult, PlayerFactsResult factsResult, PlayerBadgesResult badgesResult)
            : base("Player Details", contextResult)
        {
            DisplayName = detailsResult.DisplayName;
            DeleteUrl = detailsResult.DeleteUrl.Relative;
            DeleteEnabled = detailsResult.CanDelete;
            ShowUserInfo = detailsResult.IsUser;
            UserUrl = detailsResult.UserUrl.Relative;
            AvatarModel = new AvatarModel(detailsResult.AvatarUrl);
            InvitationUrl = detailsResult.InvitationUrl.Relative;
            PlayerBadgesModel = new PlayerBadgesModel(badgesResult);
            PlayerFactsModel = new PlayerFactsModel(factsResult);
        }
    }
}