namespace entities{

	class CashgameComparer{

		public static function compareStartTime(Cashgame $a, Cashgame $b){
			$aTimeStamp = $a.getStartTime().getTimestamp();
			$bTimeStamp = $b.getStartTime().getTimestamp();
			if ($aTimeStamp == $bTimeStamp) {
				return 0;
			}
			return ($aTimeStamp < $bTimeStamp) ? -1 : 1;
		}

	}

}