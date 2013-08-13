<?php
namespace config{

	use Mishiin\Config;

	class ConfigDatabaseTest extends Config{

		public function __construct(){
			parent::__construct();
			$this->values['mode'] = 'dev';
			$this->values['scriptTimeout'] = 10;
			$this->values['errorsEnabled'] = true;
			$this->values['databaseHost'] = '127.0.0.1';
			$this->values['databaseName'] = 'homegamemanager-integration';
			$this->values['databaseUserName'] = 'homegame';
			$this->values['databasePassword'] = 'bobb12br';
			$this->values['twitterKey'] = '';
			$this->values['twitterSecret'] = '';
		}

	}

}