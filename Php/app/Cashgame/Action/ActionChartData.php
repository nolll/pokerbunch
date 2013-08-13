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
			homegame = $homegame;
			cashgame = $cashgame;
			result = $result;
			addActionColumns();
			addActionRows();
		}

		private function addActionRows(){
			$checkpoints = getCheckpoints();
			$totalBuyin = 0;
			foreach($checkpoints as $checkpoint){
				if($checkpoint.getType() == CheckpointType::buyin){
					if($totalBuyin > 0){
						$stackBefore = $checkpoint.getStack() - $checkpoint.getAmount();
						addActionRow($checkpoint.getTimestamp(), $stackBefore, $totalBuyin);
					}
					$totalBuyin += $checkpoint.getAmount();
				}
				addActionRow($checkpoint.getTimestamp(), $checkpoint.getStack(), $totalBuyin);
			}
			if(cashgame.getStatus() == GameStatus::running){
				$timestamp = DateTimeFactory::now(homegame.getTimezone());
				addActionRow($timestamp, result.getStack(), result.getBuyin());
			}
		}

		private function getCheckpoints(){
			if(playerIsInGame()){
				return result.getCheckpoints();
			} else {
				return array();
			}
		}

		private function playerIsInGame(){
			return result != null;
		}

		private function addActionColumns(){
			$timeCol = new ChartDateTimeColumnModel('Time', 'HH:mm');
			$stackCol = new ChartNumberColumnModel('Stack');
			$buyinCol = new ChartNumberColumnModel('Buyin');
			addColumn($timeCol);
			addColumn($stackCol);
			addColumn($buyinCol);
		}

		private function addActionRow(DateTime $dateTime, $stack, $buyin){
			$row1 = new ChartRowModel();
			$row1.addValue(new ChartDateTimeValueModel($dateTime));
			$row1.addValue(new ChartValueModel($stack));
			$row1.addValue(new ChartValueModel($buyin));
			addRow($row1);
		}

	}

}