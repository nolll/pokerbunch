using Core.Classes;
using Web.Models;

namespace app{

	class UserUrlModel : UrlModel{

	    public UserUrlModel(string format, User user) : base(UrlFormatter.FormatUser(format, user))
	    {
	    }

	}

}