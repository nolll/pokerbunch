<?php
namespace entities{

	class CashgameTotalResultFactoryImpl implements CashgameTotalResultFactory{

		/**
		 * @param Player $player
		 * @param CashgameResult[] $results
		 * @return CashgameTotalResult
		 */
		public function create(Player $player, array $results){
			$cashgameTotalResult = new CashgameTotalResult();

			$winnings = 0;
			$gameCount = 0;
			$timePlayed = 0;

			foreach($results as $result){
				$winnings += $result->getWinnings();
				$gameCount++;
				$timePlayed += $result->getPlayedTime();
			}

			$winRate = $timePlayed > 0 ? round($winnings / $timePlayed * 60) : 0;

			$cashgameTotalResult->setPlayer($player);
			$cashgameTotalResult->setWinnings($winnings);
			$cashgameTotalResult->setGameCount($gameCount);
			$cashgameTotalResult->setTimePlayed($timePlayed);
			$cashgameTotalResult->setWinRate($winRate);

			return $cashgameTotalResult;
		}

	}

}