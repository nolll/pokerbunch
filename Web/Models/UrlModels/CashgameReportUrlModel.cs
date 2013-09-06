using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{
    
	class CashgameReportUrlModel : PlayerUrlModel{

        public CashgameReportUrlModel(Homegame homegame, Player player)
            : base(RouteFormats.CashgameReport, homegame, player)
    {
		}

	}

}