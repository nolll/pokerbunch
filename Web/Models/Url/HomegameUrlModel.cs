using Core.Classes;

namespace Web.Models.Url{

	public class HomegameUrlModel : UrlModel{

	    public HomegameUrlModel(string format, Homegame homegame) : base(UrlFormatter.FormatHomegame(format, homegame))
	    {
	    }

	}

}