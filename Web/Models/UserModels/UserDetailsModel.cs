using Core.Classes;
using Core.Services;
using Web.ModelFactories.MiscModelFactories;
using Web.Models.MiscModels;
using Web.Models.PageBaseModels;
using Web.Models.UrlModels;

namespace Web.Models.UserModels{

	public class UserDetailsModel : PageProperties {

		public string UserName { get; set; }
		public string DisplayName { get; set; }
		public string RealName { get; set; }
		public string Email { get; set; }
		public UrlModel EditLink { get; set; }
		public UrlModel ChangePasswordLink { get; set; }
		public bool ShowEditLink { get; set; }
		public bool ShowPasswordLink { get; set; }
		public AvatarModel AvatarModel { get; set; }

		public UserDetailsModel(User currentUser, User displayUser, IAvatarService avatarService)
            : base(currentUser)
        {
			UserName = displayUser.UserName;
			DisplayName = displayUser.DisplayName;
			RealName = displayUser.RealName;
			Email = displayUser.Email;

			var avatarModelBuilder = new AvatarModelFactory(avatarService);
			AvatarModel = avatarModelBuilder.Create(displayUser.Email);

			ShowEditLink = false;
			ShowPasswordLink = false;
			var isViewingCurrentUser = displayUser.UserName == currentUser.UserName;

			if(currentUser.IsAdmin || isViewingCurrentUser){
				ShowEditLink = true;
				EditLink = new UserEditUrlModel(displayUser);
			}

			if(isViewingCurrentUser){
				ShowPasswordLink = true;
				ChangePasswordLink = new ChangePasswordUrlModel();
			}
		}

        public override string BrowserTitle
        {
            get { return "User Details"; }
        }

	}

}