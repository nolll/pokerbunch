namespace entities{

	class CashgameTotalResultComparer{

		/**
		 * @param CashgameTotalResult $a
		 * @param CashgameTotalResult $b
		 * @return int
		 */
		public static function compareResult($a, $b){
			if ($a.getWinnings() == $b.getWinnings()) {
				return 0;
			}
			return ($a.getWinnings() > $b.getWinnings()) ? -1 : 1;
		}

	}

}