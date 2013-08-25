using Core.Classes;
using Web.Models.UrlModels;

namespace Web.Models.NavigationModels{

	public class UserNavigationModel : NavigationModel{

	    public UserNavigationModel(User user) : base("Account", null, "user-nav")
	    {
	        if(user == null){
				SetupAnonymous();
			} else {
				SetupLoggedIn(user);
			}
	    }

		private void SetupAnonymous(){
			AddNode(new NavigationNode("Sign in", new AuthLoginUrlModel()));
			AddNode(new NavigationNode("Register", new UserAddUrlModel()));
			AddNode(new NavigationNode("Forgot password", new ForgotPasswordUrlModel()));
		}

		private void SetupLoggedIn(User user){
			AddNode(new NavigationNode(user.DisplayName, new UserDetailsUrlModel(user)));
			AddNode(new NavigationNode("Sharing", new SharingSettingsUrlModel()));
			AddNode(new NavigationNode("Sign Out", new AuthLogoutUrlModel()));
		}

	}

}
