using Core.Classes;

namespace Web.Models.Url{

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