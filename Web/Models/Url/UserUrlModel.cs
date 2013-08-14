using Core.Classes;

namespace Web.Models.Url{

	class UserUrlModel : UrlModel{

	    public UserUrlModel(string format, User user) : base(UrlFormatter.FormatUser(format, user))
	    {
	    }

	}

}