using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Achievements;
using Web.Models.PlayerModels.Facts;

namespace Web.Models.PlayerModels.Details{

	public class PlayerDetailsPageModel : IPageModel {

        public string BrowserTitle { get; set; }
	    public PageProperties PageProperties { get; set; }
	    public bool ShowUserInfo { get; set; }
	    public bool ShowInvitation { get; set; }
		public string DisplayName { get; set; }
		public bool DeleteEnabled { get; set; }
		public string DeleteUrl { get; set; }
		public string UserUrl { get; set; }
		public string InvitationUrl { get; set; }
		public string UserEmail { get; set; }
		public AvatarModel AvatarModel { get; set; }
		public PlayerFactsModel PlayerFactsModel { get; set; }
		public PlayerBadgesModel PlayerBadgesModel { get; set; }
	}

}