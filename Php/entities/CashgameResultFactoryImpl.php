<?php
namespace entities{

	use DateTime;
	use entities\Checkpoints\Checkpoint;

	class CashgameResultFactoryImpl implements CashgameResultFactory{

		public function __construct(){

		}

		public function create(Player $player, array $checkpoints){
			$result = new CashgameResult();

			$buyin = $this->getBuyinSum($checkpoints);
			$stack = $this->getStack($checkpoints);
			$winnings = $stack - $buyin;
			$buyinTime = $this->getBuyinTime($checkpoints);
			$lastReportTime = $this->getLastReportTime($checkpoints);
			$cashoutCheckpoint = $this->getCashoutCheckpoint($checkpoints);
			$cashoutTime = $cashoutCheckpoint != null ? $cashoutCheckpoint->getTimeStamp() : null;
			$playedTime = $this->getPlayedTime($buyinTime, $cashoutTime);
			$hasReported = $this->hasReported($checkpoints);

			$result->setPlayer($player);
			$result->setCheckpoints($checkpoints);
			$result->setBuyin($buyin);
			$result->setStack($stack);
			$result->setWinnings($winnings);
			$result->setBuyinTime($buyinTime);
			$result->setCashoutTime($cashoutTime);
			$result->setLastReportTime($lastReportTime);
			$result->setPlayedTime($playedTime);
			$result->setCashoutCheckpoint($cashoutCheckpoint);
			$result->setHasReported($hasReported);

			return $result;
		}

		/**
		 * @param Checkpoint[] $checkpoints
		 * @return int
		 */
		private function getBuyinSum(array $checkpoints){
			$typedCheckpoints = $this->getCheckpointsOfType($checkpoints, 'entities\Checkpoints\BuyinCheckpoint');
			$buyin = 0;
			foreach($typedCheckpoints as $checkpoint){
				$buyin += $checkpoint->getAmount();
			}
			return $buyin;
		}

		/**
		 * @param Checkpoint[] $checkpoints
		 * @param $type
		 * @return Checkpoint[]
		 */
		private function getCheckpointsOfType(array $checkpoints, $type){
			$typedCheckpoints = array();
			foreach($checkpoints as $checkpoint){
				if(get_class($checkpoint) == $type){
					$typedCheckpoints[] = $checkpoint;
				}
			}
			return $typedCheckpoints;
		}

		private function getStack(array $checkpoints){
			$checkpoint = $this->getLastCheckpoint($checkpoints);
			return $checkpoint != null ? $checkpoint->getStack() : 0;
		}

		/**
		 * @param Checkpoint[] $checkpoints
		 * @return CheckPoint|null
		 */
		private function getLastCheckpoint(array $checkpoints){
			return count($checkpoints) != 0 ? end($checkpoints) : null;
		}

		private function getBuyinTime(array $checkpoints){
			$checkpoint = $this->getFirstBuyinCheckpoint($checkpoints);
			if($checkpoint == null){
				return null;
			} else {
				return $checkpoint->getTimestamp();
			}
		}

		private function getCashoutTime(array $checkpoints){
			$checkpoint = $this->getCashoutCheckpoint($checkpoints);
			if($checkpoint == null){
				return null;
			}
			return $checkpoint->getTimestamp();
		}

		private function getFirstBuyinCheckpoint(array $checkpoints){
			return $this->getCheckpointOfType($checkpoints, 'entities\Checkpoints\BuyinCheckpoint');
		}

		private function getCashoutCheckpoint(array $checkpoints){
			return $this->getCheckpointOfType($checkpoints, 'entities\Checkpoints\CashoutCheckpoint');
		}

		/**
		 * @param Checkpoint[] $checkpoints
		 * @param $type
		 * @return Checkpoint|null
		 */
		private function getCheckpointOfType(array $checkpoints, $type){
			foreach($checkpoints as $checkpoint){
				if(get_class($checkpoint) == $type){
					return $checkpoint;
				}
			}
			return null;
		}

		private function getPlayedTime(DateTime $startTime = null, DateTime $endTime = null){
			if($startTime == null || $endTime == null){
				return 0;
			}
			$timespan = $startTime->diff($endTime);
			return $timespan->h * 60 + $timespan->i;
		}

		private function getLastReportTime(array $checkpoints){
			$checkpoint = $this->getLastCheckpoint($checkpoints);
			if($checkpoint == null){
				return new DateTime();
			} else {
				return $checkpoint->getTimestamp();
			}
		}

		public function hasReported(array $checkpoints){
			$reportCheckpoints = $this->getCheckpointsOfType($checkpoints, 'entities\Checkpoints\ReportCheckpoint');
			return count($reportCheckpoints) > 0;
		}

	}

}