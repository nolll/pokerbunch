using Core.Classes;
using Web.Formatters;

namespace Web.Models.UrlModels{

	public class HomegameUrlModel : UrlModel{

	    public HomegameUrlModel(string format, Homegame homegame) : base(UrlFormatter.FormatHomegame(format, homegame))
	    {
	    }

	}

}