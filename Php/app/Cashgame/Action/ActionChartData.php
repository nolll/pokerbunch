<?php
namespace app\Cashgame\Action {

	use DateTime;
	use app\Chart\ChartDateTimeColumnModel;
	use app\Chart\ChartDateTimeValueModel;
	use app\Chart\ChartModel;
	use app\Chart\ChartNumberColumnModel;
	use app\Chart\ChartRowModel;
	use app\Chart\ChartValueModel;
	use core\DateTimeFactory;
	use entities\Cashgame;
	use entities\CashgameResult;
	use entities\Checkpoints\CheckpointType;
	use entities\GameStatus;
	use entities\Homegame;

	class ActionChartData extends ChartModel{

		private $homegame;
		private $cashgame;
		private $result;

		public function __construct(Homegame $homegame, Cashgame $cashgame, CashgameResult $result){
			$this->homegame = $homegame;
			$this->cashgame = $cashgame;
			$this->result = $result;
			$this->addActionColumns();
			$this->addActionRows();
		}

		private function addActionRows(){
			$checkpoints = $this->getCheckpoints();
			$totalBuyin = 0;
			foreach($checkpoints as $checkpoint){
				if($checkpoint->getType() == CheckpointType::buyin){
					if($totalBuyin > 0){
						$stackBefore = $checkpoint->getStack() - $checkpoint->getAmount();
						$this->addActionRow($checkpoint->getTimestamp(), $stackBefore, $totalBuyin);
					}
					$totalBuyin += $checkpoint->getAmount();
				}
				$this->addActionRow($checkpoint->getTimestamp(), $checkpoint->getStack(), $totalBuyin);
			}
			if($this->cashgame->getStatus() == GameStatus::running){
				$timestamp = DateTimeFactory::now($this->homegame->getTimezone());
				$this->addActionRow($timestamp, $this->result->getStack(), $this->result->getBuyin());
			}
		}

		private function getCheckpoints(){
			if($this->playerIsInGame()){
				return $this->result->getCheckpoints();
			} else {
				return array();
			}
		}

		private function playerIsInGame(){
			return $this->result != null;
		}

		private function addActionColumns(){
			$timeCol = new ChartDateTimeColumnModel('Time', 'HH:mm');
			$stackCol = new ChartNumberColumnModel('Stack');
			$buyinCol = new ChartNumberColumnModel('Buyin');
			$this->addColumn($timeCol);
			$this->addColumn($stackCol);
			$this->addColumn($buyinCol);
		}

		private function addActionRow(DateTime $dateTime, $stack, $buyin){
			$row1 = new ChartRowModel();
			$row1->addValue(new ChartDateTimeValueModel($dateTime));
			$row1->addValue(new ChartValueModel($stack));
			$row1->addValue(new ChartValueModel($buyin));
			$this->addRow($row1);
		}

	}

}