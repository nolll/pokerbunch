using Core.Classes;
using Web.Formatters;

namespace Web.Models.UrlModels{
    public class CashgameUrlModel : UrlModel{

		public CashgameUrlModel(string format, Homegame homegame, Cashgame cashgame)
        {
            SetUrl(UrlFormatter.FormatCashgame(format, homegame, cashgame));
		}

	}

}