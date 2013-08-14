using Core.Classes;
using Web.Routing;
using app;

namespace Web.Models{

	class UserDetailsUrlModel : UserUrlModel{

		public UserDetailsUrlModel(User user) : base(RouteFormats.UserDetails, user) {
		}

	}

}