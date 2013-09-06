using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{
    public class CashgameReportUrlModel : PlayerUrlModel{

        public CashgameReportUrlModel(Homegame homegame, Player player)
            : base(RouteFormats.CashgameReport, homegame, player)
    {
		}

	}

}