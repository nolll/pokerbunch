<?php
namespace app{

	use core\Globalization;
	use DateTime;
	use entities\Homegame;
	use entities\Cashgame;
	use entities\Player;
	use Domain\Classes\User;

	class UrlFormatter{

		public static function formatHomegame($format, Homegame $homegame){
			return sprintf($format, $homegame->getSlug());
		}

		public static function formatHomegameWithYear($format, Homegame $homegame, $year){
			return sprintf($format, $homegame->getSlug(), $year);
		}

		public static function formatCashgame($format, Homegame $homegame, Cashgame $cashgame){
			$isoDate = self::formatIsoDate($cashgame->getStartTime());
			return sprintf($format, $homegame->getSlug(), $isoDate);
		}

		public static function formatPlayer($format, Homegame $homegame, Player $player){
			$encodedPlayerName = rawurlencode($player->getDisplayName());
			return sprintf($format, $homegame->getSlug(), $encodedPlayerName);
		}

		public static function formatUser($format, User $user){
			return sprintf($format, $user->getUserName());
		}

		public static function formatIsoDate(DateTime $date){
			return Globalization::formatIsoDate($date);
		}

	}

}