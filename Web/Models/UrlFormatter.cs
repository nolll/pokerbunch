using System;
using Core.Classes;

namespace Web.Models{

	public class UrlFormatter{

		public static string FormatHomegame(string format, Homegame homegame){
			return string.Format(format, homegame.Slug);
		}

		public static string FormatHomegameWithYear(string format, Homegame homegame, int year){
			return string.Format(format, homegame.Slug, year);
		}

        /*
		public static function formatCashgame($format, Homegame $homegame, Cashgame $cashgame){
			$isoDate = self::formatIsoDate($cashgame.getStartTime());
			return sprintf($format, $homegame.getSlug(), $isoDate);
		}
        */

        /*
		public static function formatPlayer($format, Homegame $homegame, Player $player){
			$encodedPlayerName = rawurlencode($player.getDisplayName());
			return sprintf($format, $homegame.getSlug(), $encodedPlayerName);
		}
        */

		public static string FormatUser(string format, User user){
			return string.Format(format, user.UserName);
		}

        /*
		public static function formatIsoDate(DateTime $date){
			return Infrastructure.System.Globalization::formatIsoDate($date);
		}
        */

	}

}