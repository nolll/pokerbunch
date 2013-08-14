using Core.Classes;
using Web.Models.Url;

namespace Web.Models.Navigation{

	public class AdminNavModel : NavigationModel {

	    public AdminNavModel(User user) : base("Admin", null, "admin-nav")
	    {
	        var isAdmin = user != null && user.IsAdmin;
			if(isAdmin){
				const bool selected = false;
				AddNode(new NavigationNode("Bunches", new HomegameListingUrlModel(), selected));
				AddNode(new NavigationNode("Users", new UserListingUrlModel(), selected));
			}
	    }

	}

}