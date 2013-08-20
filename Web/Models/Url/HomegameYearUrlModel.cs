using Core.Classes;
using Web.Models;
using Web.Models.Url;

namespace app{

	class HomegameYearUrlModel : UrlModel{

	    public HomegameYearUrlModel(string format, string formatWithYear, Homegame homegame, int? year)
	    {
            if (year.HasValue)
            {
                SetUrl(UrlFormatter.FormatHomegameWithYear(formatWithYear, homegame, year.Value));
            }
            else
            {
                SetUrl(UrlFormatter.FormatHomegame(format, homegame));
            }
	    }
	}

}