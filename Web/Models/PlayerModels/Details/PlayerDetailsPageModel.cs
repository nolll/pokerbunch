using Core.UseCases;
using Web.Components.PlayerModels.Badges;
using Web.Extensions;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Facts;
using Web.Urls.SiteUrls;

namespace Web.Models.PlayerModels.Details
{
    public class PlayerDetailsPageModel : BunchPageModel
    {
        public string DisplayName { get; }
        public string DeleteUrl { get; }
        public bool ShowUserInfo { get; }
        public bool DeleteEnabled { get; }
        public string UserUrl { get; }
        public string InvitationUrl { get; }
        public AvatarModel AvatarModel { get; }
        public PlayerFactsModel PlayerFactsModel { get; }
        public BadgeListModel BadgeListModel { get; }
        public string Color { get; }

        public PlayerDetailsPageModel(BunchContext.Result contextResult, PlayerDetails.Result detailsResult, PlayerFacts.Result factsResult, PlayerBadges.Result badgesResult)
            : base(contextResult)
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
            Color = detailsResult.Color;
        }

        public override string BrowserTitle => "Player Details";

        public override View GetView()
        {
            return new View("~/Views/Pages/PlayerDetails/Details.cshtml");
        }
    }
}