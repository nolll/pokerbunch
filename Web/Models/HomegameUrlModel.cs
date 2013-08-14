using Core.Classes;

namespace Web.Models{

	public class HomegameUrlModel : UrlModel{

	    public HomegameUrlModel(string format, Homegame homegame) : base(UrlFormatter.FormatHomegame(format, homegame))
	    {
	    }

	}

}