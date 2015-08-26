using Core.UseCases;
using Web.Components.PlayerModels.Badges;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Facts;
using Web.Urls;

namespace Web.Models.PlayerModels.Details
{
    public class PlayerDetailsPageModel : BunchPageModel
    {
        public string DisplayName { get; private set; }
        public string DeleteUrl { get; private set; }
        public bool ShowUserInfo { get; private set; }
        public bool DeleteEnabled { get; private set; }
        public string UserUrl { get; private set; }
        public string InvitationUrl { get; private set; }
        public AvatarModel AvatarModel { get; private set; }
        public PlayerFactsModel PlayerFactsModel { get; private set; }
        public BadgeListModel BadgeListModel { get; private set; }

        public PlayerDetailsPageModel(BunchContext.Result contextResult, PlayerDetails.Result detailsResult, PlayerFacts.Result factsResult, PlayerBadges.Result badgesResult)
            : base("Player Details", contextResult)
        {
            DisplayName = detailsResult.DisplayName;
            DeleteUrl = new DeletePlayerUrl(detailsResult.PlayerId).Relative;
            DeleteEnabled = detailsResult.CanDelete;
            ShowUserInfo = detailsResult.IsUser;
            UserUrl = detailsResult.IsUser ? new UserDetailsUrl(detailsResult.UserName).Relative : string.Empty;
            AvatarModel = new AvatarModel(detailsResult.AvatarUrl);
            InvitationUrl = new InvitePlayerUrl(detailsResult.PlayerId).Relative;
            BadgeListModel = new BadgeListModel(badgesResult);
            PlayerFactsModel = new PlayerFactsModel(factsResult);
        }
    }
}