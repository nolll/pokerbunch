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
			homegame = $homegame;
			cashgame = $cashgame;
			addActionColumns();
			addActionRows();
		}

		private function addActionRows(){
			$results = cashgame.getResults();
			foreach($results as $result){
				$totalBuyin = 0;
				$checkpoints = $result.getCheckpoints();
				$playerName = $result.getPlayer().getDisplayName();
				foreach($checkpoints as $checkpoint){
					if($checkpoint.getType() == CheckpointType::buyin){
						$totalBuyin += $checkpoint.getAmount();
					}
					addActionRow($checkpoint.getTimestamp(), $checkpoint.getStack() - $totalBuyin, $playerName);
				}
			}
			if(cashgame.getStatus() == GameStatus::running){
				addCurrentStacks($results);
			}
		}

		private function addCurrentStacks($results){
			$timestamp = DateTimeFactory::now(homegame.getTimezone());
			$row = new ChartRowModel();
			$row.addValue(new ChartDateTimeValueModel($timestamp));
			foreach($results as $result){  /** @var $result CashgameResult */
				$winnings = $result.getStack() - $result.getBuyin();
				$row.addValue(new ChartValueModel($winnings));
			}
			addRow($row);
		}

		private function addActionColumns(){
			$timeCol = new ChartDateTimeColumnModel('Time', 'HH:mm');
			addColumn($timeCol);
			$playerNames = cashgame.getPlayerNames();
			foreach($playerNames as $playerName){
				$playerCol = new ChartNumberColumnModel($playerName);
				addColumn($playerCol);
			}
		}

		private function addActionRow(DateTime $dateTime, $winnings, $currentPlayerName){
			$row1 = new ChartRowModel();
			$row1.addValue(new ChartDateTimeValueModel($dateTime));
			$playerNames = cashgame.getPlayerNames();
			foreach($playerNames as $playerName){
				$val = null;
				if($playerName == $currentPlayerName){
					$val = $winnings;
				}
				$row1.addValue(new ChartValueModel($val));
			}
			addRow($row1);
		}

	}

}