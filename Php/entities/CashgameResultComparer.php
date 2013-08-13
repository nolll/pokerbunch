namespace entities{

	class CashgameResultComparer{

		public static function compareName(CashgameResult $a, CashgameResult $b){
			if ($a->getPlayer()->getDisplayName() == $b->getPlayer()->getDisplayName()) {
				return 0;
			}
			return ($a->getPlayer()->getDisplayName() < $b->getPlayer()->getDisplayName()) ? -1 : 1;
		}

		public static function compareResult(CashgameResult $a, CashgameResult $b){
			$aWinnings = $a->getStack() - $a->getBuyin();
			$bWinnings = $b->getStack() - $b->getBuyin();
			if ($aWinnings == $bWinnings) {
				return 0;
			}
			return ($aWinnings > $bWinnings) ? -1 : 1;
		}

		public static function compareStack(CashgameResult $a, CashgameResult $b){
			$aStack = $a->getStack();
			$bStack = $b->getStack();
			if ($aStack == $bStack) {
				return 0;
			}
			return ($aStack > $bStack) ? -1 : 1;
		}

		public static function compareWinnings(CashgameResult $a, CashgameResult $b){
			$aWinnings = $a->getWinnings();
			$bWinnings = $b->getWinnings();
			if ($aWinnings == $bWinnings) {
				return 0;
			}
			return ($aWinnings > $bWinnings) ? -1 : 1;
		}

	}

}