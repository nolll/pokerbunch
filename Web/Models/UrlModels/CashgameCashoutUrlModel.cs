using Core.Classes;
using Web.Routing;

namespace Web.Models.UrlModels{

	public class CashgameCashoutUrlModel : PlayerUrlModel
	{

	    public CashgameCashoutUrlModel(Homegame homegame, Player player)
	        : base(RouteFormats.CashgameCashout, homegame, player)
    	{
	    }

	}

}