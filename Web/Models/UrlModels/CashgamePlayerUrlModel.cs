using System.Web;
using Core.Classes;
using Web.Formatters;

namespace Web.Models.UrlModels{

	public class CashgamePlayerUrlModel : UrlModel{

		public CashgamePlayerUrlModel(string format, Homegame homegame, Cashgame cashgame, Player player){
			var isoDate = cashgame.StartTime.HasValue ? UrlFormatter.FormatIsoDate(cashgame.StartTime.Value) : string.Empty;
			var encodedPlayerName = HttpUtility.UrlPathEncode(player.DisplayName);
			var url = string.Format(format, homegame.Slug, isoDate, encodedPlayerName);
		    SetUrl(url);
		}

	}

}