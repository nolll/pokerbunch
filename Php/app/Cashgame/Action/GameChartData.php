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

	class GameChartData extends ChartModel{

		private $homegame;
		private $cashgame;

		public function __construct(Homegame $homegame, Cashgame $cashgame){
			$this->homegame = $homegame;
			$this->cashgame = $cashgame;
			$this->addActionColumns();
			$this->addActionRows();
		}

		private function addActionRows(){
			$results = $this->cashgame->getResults();
			foreach($results as $result){
				$totalBuyin = 0;
				$checkpoints = $result->getCheckpoints();
				$playerName = $result->getPlayer()->getDisplayName();
				foreach($checkpoints as $checkpoint){
					if($checkpoint->getType() == CheckpointType::buyin){
						$totalBuyin += $checkpoint->getAmount();
					}
					$this->addActionRow($checkpoint->getTimestamp(), $checkpoint->getStack() - $totalBuyin, $playerName);
				}
			}
			if($this->cashgame->getStatus() == GameStatus::running){
				$this->addCurrentStacks($results);
			}
		}

		private function addCurrentStacks($results){
			$timestamp = DateTimeFactory::now($this->homegame->getTimezone());
			$row = new ChartRowModel();
			$row->addValue(new ChartDateTimeValueModel($timestamp));
			foreach($results as $result){  /** @var $result CashgameResult */
				$winnings = $result->getStack() - $result->getBuyin();
				$row->addValue(new ChartValueModel($winnings));
			}
			$this->addRow($row);
		}

		private function addActionColumns(){
			$timeCol = new ChartDateTimeColumnModel('Time', 'HH:mm');
			$this->addColumn($timeCol);
			$playerNames = $this->cashgame->getPlayerNames();
			foreach($playerNames as $playerName){
				$playerCol = new ChartNumberColumnModel($playerName);
				$this->addColumn($playerCol);
			}
		}

		private function addActionRow(DateTime $dateTime, $winnings, $currentPlayerName){
			$row1 = new ChartRowModel();
			$row1->addValue(new ChartDateTimeValueModel($dateTime));
			$playerNames = $this->cashgame->getPlayerNames();
			foreach($playerNames as $playerName){
				$val = null;
				if($playerName == $currentPlayerName){
					$val = $winnings;
				}
				$row1->addValue(new ChartValueModel($val));
			}
			$this->addRow($row1);
		}

	}

}