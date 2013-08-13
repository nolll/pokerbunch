namespace config{

	use Mishiin\Config;

	class ConfigProduction extends Config{

		public function __construct(){
			parent::__construct();
			$this->values['mode'] = 'prod';
			$this->values['scriptTimeout'] = 10;
			$this->values['errorsEnabled'] = false;
			$this->values['databaseHost'] = '127.0.0.1';
			$this->values['databaseName'] = 'homegamemanager';
			$this->values['databaseUserName'] = 'root';
			$this->values['databasePassword'] = '3sugfisk';
			$this->values['twitterKey'] = 'pEqtXqMKnClsmtMgw3t2iA';
			$this->values['twitterSecret'] = 'oqODUahzHDsBSLEiWqmfBRBgTmUydRoBEIVroXi8';
		}

	}

}