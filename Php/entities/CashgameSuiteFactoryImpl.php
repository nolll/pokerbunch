namespace entities{

	class CashgameSuiteFactoryImpl implements CashgameSuiteFactory{

		private $cashgameTotalResultFactory;

		public function __construct(CashgameTotalResultFactory $cashgameTotalResultFactory){
			$this->cashgameTotalResultFactory = $cashgameTotalResultFactory;
		}

		public function create($cashgames, $players){
			$suite = new CashgameSuite();

			$totalGameTime = 0;
			$resultIndex = $this->getPlayerIndex($players);
			/** @var CashgameResult $bestResult */
			$bestResult = null;
			/** @var CashgameResult $worstResult */
			$worstResult = null;

			usort($cashgames, 'entities\CashgameComparer::compareStartTime');
			$cashgames = array_reverse($cashgames);
			foreach($cashgames as $cashgame){ /** @var $cashgame Cashgame */
				$results = $cashgame->getResults();
				foreach($results as $result){
					$resultIndex[$result->getPlayer()->getId()][] = $result;
					if($bestResult == null || $result->getWinnings() > $bestResult->getWinnings()){
						$bestResult = $result;
					}
					if($worstResult == null || $result->getWinnings() < $worstResult->getWinnings()){
						$worstResult = $result;
					}
				}
				$duration = $cashgame->getDuration();
				$totalGameTime += $duration;
			}

			$totalResults = $this->getTotalResults($players, $resultIndex);
			$mostTimeResult = $this->getMostTimeResult($totalResults);
			$bestTotalResult = reset($totalResults);
			if(!$bestTotalResult){
				$bestTotalResult = null;
			}

			$suite->setTotalResults($totalResults);
			$suite->setCashgames($cashgames);
			$suite->setGameCount(count($cashgames));
			$suite->setTotalGameTime($totalGameTime);
			$suite->setBestResult($bestResult);
			$suite->setWorstResult($worstResult);
			$suite->setMostTimeResult($mostTimeResult);
			$suite->setBestTotalResult($bestTotalResult);

			return $suite;
		}

		/**
		 * @param Player[] $players
		 * @return array
		 */
		private function getPlayerIndex(array $players){
			$hashtable = array();
			foreach($players as $player){ /** @var $player Player */
				$hashtable[$player->getId()] = array();
			}
			return $hashtable;
		}

		/**
		 * @param Player[] $players
		 * @param array $resultIndex
		 * @return array
		 */
		private function getTotalResults(array $players, array $resultIndex){
			$totalResults = array();
			foreach($players as $player){
				$playerResults = $resultIndex[$player->getId()];
				if(count($playerResults) > 0){
					$totalResults[] = $this->cashgameTotalResultFactory->create($player, $playerResults);
				}
			}
			usort($totalResults, 'entities\CashgameTotalResultComparer::compareResult');
			return $totalResults;
		}

		/**
		 * @param CashgameTotalResult[] $results
		 * @return CashgameTotalResult
		 */
		public function getMostTimeResult(array $results){
			/** @var CashgameTotalResult $mostTimeResult */
			$mostTimeResult = null;
			foreach($results as $result){
				if($mostTimeResult == null || $result->getTimePlayed() > $mostTimeResult->getTimePlayed()){
					$mostTimeResult = $result;
				}
			}
			return $mostTimeResult;
		}

	}

}