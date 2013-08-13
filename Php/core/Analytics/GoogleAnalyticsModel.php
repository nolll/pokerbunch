namespace core\Analytics{

	use Mishiin\Environment;

	class GoogleAnalyticsModel{

		public $enableAnalytics;

		public function __construct(){
			enableAnalytics = isInProduction();
		}

		private function isInProduction(){
			return Environment::getHost() == 'pokerbunch.com';
		}

	}

}