using System;
using Core.Classes;

namespace Web.Models{

	public class UrlFormatter{

		public static string FormatHomegame(string format, Homegame homegame){
			return string.Format(format, homegame.Slug);
		}

        /*
		public static function formatHomegameWithYear($format, Homegame $homegame, $year){
			return sprintf($format, $homegame.getSlug(), $year);
		}

		public static function formatCashgame($format, Homegame $homegame, Cashgame $cashgame){
			$isoDate = self::formatIsoDate($cashgame.getStartTime());
			return sprintf($format, $homegame.getSlug(), $isoDate);
		}

		public static function formatPlayer($format, Homegame $homegame, Player $player){
			$encodedPlayerName = rawurlencode($player.getDisplayName());
			return sprintf($format, $homegame.getSlug(), $encodedPlayerName);
		}

		public static function formatUser($format, User $user){
			return sprintf($format, $user.getUserName());
		}

		public static function formatIsoDate(DateTime $date){
			return Infrastructure.System.Globalization::formatIsoDate($date);
		}
        */

	}

}