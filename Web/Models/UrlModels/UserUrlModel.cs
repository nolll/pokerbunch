using Core.Classes;
using Web.Formatters;

namespace Web.Models.UrlModels{

	class UserUrlModel : UrlModel{

	    public UserUrlModel(string format, User user) : base(UrlFormatter.FormatUser(format, user))
	    {
	    }

	}

}