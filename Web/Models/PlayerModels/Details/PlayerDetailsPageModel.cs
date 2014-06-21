using Application.Urls;
using Application.UseCases.BunchContext;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Badges;
using Web.Models.PlayerModels.Facts;

namespace Web.Models.PlayerModels.Details
{
    public class PlayerDetailsPageModel : PageModel
    {
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

        public PlayerDetailsPageModel(BunchContextResult contextResult)
            : base("Player Details", contextResult)
        {
        }
    }
}