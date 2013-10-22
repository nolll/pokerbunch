using System;
using System.Web;
using Core.Classes;
using Infrastructure.System;

namespace Web.Formatters{

	public static class UrlFormatter{

		public static string FormatHomegame(string format, Homegame homegame){
			return string.Format(format, homegame.Slug);
		}

		public static string FormatHomegameWithYear(string format, Homegame homegame, int year){
			return string.Format(format, homegame.Slug, year);
		}

		public static string FormatCashgame(string format, Homegame homegame, Cashgame cashgame){
            if (cashgame.StartTime.HasValue)
            {
                var isoDate = FormatIsoDate(cashgame.StartTime.Value);
                return string.Format(format, homegame.Slug, isoDate);
            }
		    return null;
		}

		public static string FormatPlayer(string format, Homegame homegame, Player player){
			var encodedPlayerName = HttpUtility.UrlPathEncode(player.DisplayName);
            return string.Format(format, homegame.Slug, encodedPlayerName);
		}

		public static string FormatUser(string format, User user){
			return string.Format(format, user.UserName);
		}

    	public static string FormatIsoDate(DateTime date){
			return StaticGlobalization.FormatIsoDate(date);
		}

	}

}