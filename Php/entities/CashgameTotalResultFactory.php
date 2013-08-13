<?php
namespace entities{

	interface CashgameTotalResultFactory{

		/**
		 * @param Player $player
		 * @param CashgameResult[] $results
		 * @return CashgameTotalResult
		 */
		public function create(Player $player, array $results);

	}

}