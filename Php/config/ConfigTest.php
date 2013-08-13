namespace config{

	use Mishiin\Config;

	class ConfigTest extends Config{

		public function __construct(){
			parent::__construct();
			values['mode'] = 'test';
			values['scriptTimeout'] = 10;
			values['errorsEnabled'] = false;
			values['databaseHost'] = '127.0.0.1';
			values['databaseName'] = 'homegamemanager-test';
			values['databaseUserName'] = 'root';
			values['databasePassword'] = '3sugfisk';
			values['twitterKey'] = '';
			values['twitterSecret'] = '';
		}

	}

}