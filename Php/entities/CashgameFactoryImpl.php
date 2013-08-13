<?php
namespace entities{

	use DateTime;

	class CashgameFactoryImpl implements CashgameFactory{

		public function create($location, $status = null, $id = null, array $results = null){
			if($status == null){
				$status = GameStatus::created;
			}
			if($results == null){
				$results = array();
			}

			$cashgame = new Cashgame();
			$cashgame->setLocation($location);
			$cashgame->setStatus($status);
			$cashgame->setId($id);

			$cashgame->setResults($results);
			$playerCount = count($results);
			$cashgame->setNumPlayers($playerCount);

			$startTime = $this->getStartTime($results);
			$endTime = $this->getEndTime($results);
			$cashgame->setStartTime($startTime);
			$cashgame->setEndTime($endTime);
			$cashgame->setDuration($this->getDuration($startTime, $endTime));
			$cashgame->setIsStarted($startTime != null);

			$buyinSum = $this->getBuyinSum($results);
			$cashoutSum = $this->getCashoutSum($results);
			$cashgame->setTurnOver($buyinSum);
			$cashgame->setDiff($buyinSum - $cashoutSum);

			$cashgame->setHasActivePlayers($this->hasActivePlayers($results));
			$cashgame->setTotalStacks($this->getTotalStacks($results));
			$cashgame->setAverageBuyin($this->getAverageBuyin($buyinSum, $playerCount));

			return $cashgame;
		}

		private function getStartTime(array $results){
			$startTime = null;
			foreach($results as $result){  /** @var $result CashgameResult */
				if($startTime == null || $result->getBuyinTime() < $startTime){
					$startTime = $result->getBuyinTime();
				}
			}
			return $startTime;
		}

		private function getEndTime(array $results){
			$endTime = null;
			foreach($results as $result){ /** @var $result CashgameResult */
				if($endTime == null || $result->getCashoutTime() > $endTime){
					$endTime = $result->getCashoutTime();
				}
			}
			return $endTime;
		}

		private function getDuration(DateTime $startTime = null, DateTime $endTime = null){
			if($startTime == null || $endTime == null){
				return 0;
			}
			$timespan = $startTime->diff($endTime);
			return $timespan->h * 60 + $timespan->i;
		}

		private function getBuyinSum(array $results){
			$buyinSum = 0;
			foreach($results as $result){ /** @var $result CashgameResult */
				$buyinSum += $result->getBuyin();
			}
			return $buyinSum;
		}

		private function getCashoutSum(array $results){
			$cashoutSum = 0;
			foreach($results as $result){ /** @var $result CashgameResult */
				$cashoutSum += $result->getStack();
			}
			return $cashoutSum;
		}

		private function hasActivePlayers(array $results){
			foreach($results as $result){ /** @var $result CashgameResult */
				if($result->getCashoutTime() == null){
					return true;
				}
			}
			return false;
		}

		private function getTotalStacks(array $results){
			$sum = 0;
			foreach($results as $result){ /** @var $result CashgameResult */
				$sum += $result->getStack();
			}
			return $sum;
		}

		private function getAverageBuyin($turnover, $playerCount){
			if($playerCount == 0){
				return 0;
			}
			return round($turnover / $playerCount);
		}

	}

}