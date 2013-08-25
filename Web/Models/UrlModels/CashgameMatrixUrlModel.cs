using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	class CashgameMatrixUrlModel : HomegameYearUrlModel{

	    public CashgameMatrixUrlModel(Homegame homegame, int year) : base(RouteFormats.CashgameMatrix, RouteFormats.CashgameMatrixWithYear, homegame, year)
	    {
	        
	    }

	}

}