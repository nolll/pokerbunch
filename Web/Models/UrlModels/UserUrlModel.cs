using Core.Classes;
using Web.Formatters;

namespace Web.Models.UrlModels{
    public class UserUrlModel : UrlModel{

	    public UserUrlModel(string format, User user) : base(UrlFormatter.FormatUser(format, user))
	    {
	    }

	}

}