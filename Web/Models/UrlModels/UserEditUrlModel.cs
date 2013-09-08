using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class UserEditUrlModel : UserUrlModel{

		public UserEditUrlModel(User user)
            : base(RouteFormats.UserEdit, user)
        {
		}

	}

}