using Application.Urls;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Badges;
using Web.Models.PlayerModels.Facts;
using Web.Models.UrlModels;

namespace Web.Models.PlayerModels.Details
{
    public class PlayerDetailsPageModel : IPageModel
    {
        public string BrowserTitle { get; set; }
        public PageProperties PageProperties { get; set; }
        public bool ShowUserInfo { get; set; }
        public string DisplayName { get; set; }
        public bool DeleteEnabled { get; set; }
        public Url DeleteUrl { get; set; }
        public Url UserUrl { get; set; }
        public Url InvitationUrl { get; set; }
        public string UserEmail { get; set; }
        public AvatarModel AvatarModel { get; set; }
        public PlayerFactsModel PlayerFactsModel { get; set; }
        public PlayerBadgesModel PlayerBadgesModel { get; set; }
    }
}