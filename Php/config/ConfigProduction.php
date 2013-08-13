namespace config{

	use Mishiin\Config;

	class ConfigProduction extends Config{

		public function __construct(){
			parent::__construct();
			values['mode'] = 'prod';
			values['scriptTimeout'] = 10;
			values['errorsEnabled'] = false;
			values['databaseHost'] = '127.0.0.1';
			values['databaseName'] = 'homegamemanager';
			values['databaseUserName'] = 'root';
			values['databasePassword'] = '3sugfisk';
			values['twitterKey'] = 'pEqtXqMKnClsmtMgw3t2iA';
			values['twitterSecret'] = 'oqODUahzHDsBSLEiWqmfBRBgTmUydRoBEIVroXi8';
		}

	}

}