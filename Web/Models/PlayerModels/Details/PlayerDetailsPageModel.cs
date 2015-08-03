using Core.UseCases;
using Core.UseCases.BunchContext;
using Core.UseCases.PlayerBadges;
using Core.UseCases.PlayerFacts;
using Web.Components.PlayerModels.Badges;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Facts;

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

        public PlayerDetailsPageModel(BunchContextResult contextResult, PlayerDetails.Result detailsResult, PlayerFactsResult factsResult, PlayerBadgesResult badgesResult)
            : base("Player Details", contextResult)
        {
            DisplayName = detailsResult.DisplayName;
            DeleteUrl = detailsResult.DeleteUrl.Relative;
            DeleteEnabled = detailsResult.CanDelete;
            ShowUserInfo = detailsResult.IsUser;
            UserUrl = detailsResult.UserUrl.Relative;
            AvatarModel = new AvatarModel(detailsResult.AvatarUrl);
            InvitationUrl = detailsResult.InvitationUrl.Relative;
            BadgeListModel = new BadgeListModel(badgesResult);
            PlayerFactsModel = new PlayerFactsModel(factsResult);
        }
    }
}