using System.Collections.Generic;
using Core.Classes;
using Web.ModelFactories.MiscModelFactories;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Models.PlayerModels.Achievements;
using Web.Models.PlayerModels.Facts;
using Web.Models.UrlModels;

namespace Web.Models.PlayerModels.Details{

	public class PlayerDetailsPageModel : HomegamePageModel {

	    public bool ShowUserInfo { get; set; }
	    public bool ShowInvitation { get; set; }
		public string DisplayName { get; set; }
		public bool DeleteEnabled { get; set; }
		public UrlModel DeleteUrl { get; set; }
		public UrlModel UserUrl { get; set; }
		public UrlModel InvitationUrl { get; set; }
		public string UserEmail { get; set; }
		public string AvatarUrl { get; set; }
		public AvatarModel AvatarModel { get; set; }
		public PlayerFactsModel PlayerFactsModel { get; set; }
		public PlayerBadgesModel PlayerBadgesModel { get; set; }

		public PlayerDetailsPageModel(
            User currentUser,
			Homegame homegame,
			Player player,
			User user,
			List<Cashgame> cashgames,
			bool isManager,
			bool hasPlayed,
			IAvatarModelFactory avatarModelFactory,
			Cashgame runningGame = null) : base(currentUser, homegame, runningGame)
        {
			DisplayName = player.DisplayName;
			DeleteUrl = new PlayerDeleteUrlModel(homegame, player);
			DeleteEnabled = isManager && !hasPlayed;
			var hasUser = user != null;
			ShowUserInfo = hasUser;
			ShowInvitation = !hasUser;
			if(hasUser){
				UserUrl = new UserDetailsUrlModel(user);
				UserEmail = user.Email;
				AvatarModel = avatarModelFactory.Create(user.Email);
			} else {
				InvitationUrl = new PlayerInviteUrlModel(homegame, player);
			}
			PlayerFactsModel = new PlayerFactsModel(homegame, cashgames, player);
			PlayerBadgesModel = new PlayerBadgesModel(player, cashgames);
		}

        public override string BrowserTitle
        {
            get
            {
                return "Player Details";
            }
        }

	}

}