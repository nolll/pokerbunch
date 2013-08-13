namespace config{

	use Mishiin\Config;

	class ConfigDevelopment extends Config{

		public function __construct(){
			parent::__construct();
			values['mode'] = 'dev';
			values['scriptTimeout'] = 10;
			values['errorsEnabled'] = true;
			values['databaseHost'] = '127.0.0.1';
			values['databaseName'] = 'homegamemanager';
			values['databaseUserName'] = 'homegame';
			values['databasePassword'] = 'bobb12br';
			values['twitterKey'] = '';
			values['twitterSecret'] = '';
		}

	}

}