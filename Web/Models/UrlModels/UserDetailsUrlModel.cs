using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{
    public class UserDetailsUrlModel : UserUrlModel{

		public UserDetailsUrlModel(User user) : base(RouteFormats.UserDetails, user) {
		}

	}

}