namespace app\Chart{

	use DateTime;

	class ChartDateTimeValueModel extends ChartValueModel {

		public function __construct(DateTime $dateTime){
			parent::__construct($this->formatDate($dateTime));
		}

		private function formatDate(DateTime $dateTime){
			$format = 'Date(%1$s, %2$s, %3$s, %4$s, %5$s, %6$s)';
			$year = $dateTime->format('Y');
			$month = $dateTime->format('n') - 1;
			$day = $dateTime->format('j');
			$hour = $dateTime->format('G');
			$minute = intval($dateTime->format('i'));
			$second = intval($dateTime->format('s'));
			return sprintf($format, $year, $month, $day, $hour, $minute, $second);
		}

	}

}