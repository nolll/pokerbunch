using Core.Classes;
using Web.Models;
using Web.Models.Url;

namespace app{
    public class CashgameUrlModel : UrlModel{

		public CashgameUrlModel(string format, Homegame homegame, Cashgame cashgame)
        {
            SetUrl(UrlFormatter.FormatCashgame(format, homegame, cashgame));
		}

	}

}