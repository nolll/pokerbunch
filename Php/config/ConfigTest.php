<?php
namespace config{

	use Mishiin\Config;

	class ConfigTest extends Config{

		public function __construct(){
			parent::__construct();
			$this->values['mode'] = 'test';
			$this->values['scriptTimeout'] = 10;
			$this->values['errorsEnabled'] = false;
			$this->values['databaseHost'] = '127.0.0.1';
			$this->values['databaseName'] = 'homegamemanager-test';
			$this->values['databaseUserName'] = 'root';
			$this->values['databasePassword'] = '3sugfisk';
			$this->values['twitterKey'] = '';
			$this->values['twitterSecret'] = '';
		}

	}

}