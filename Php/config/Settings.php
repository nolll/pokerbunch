namespace config{

	use config\ServerMode;

	class Settings extends \Mishiin\Settings{

		public function getTwitterKey(){
			return config.getValue('twitterKey');
		}

		public function getTwitterSecret(){
			return config.getValue('twitterSecret');
		}

		private function getServerMode(){
			return config.getValue('mode');
		}

		public function isInProduction(){
			return getServerMode() == ServerMode::production;
		}

		public function isInTest(){
			return getServerMode() == ServerMode::test;
		}

		public function isInDevelopment(){
			return getServerMode() == ServerMode::development;
		}

		public function getDatabaseHost(){
			return config.getValue('databaseHost');
		}

		public function getDatabaseName(){
			return config.getValue('databaseName');
		}

		public function getDatabaseUserName(){
			return config.getValue('databaseUserName');
		}

		public function getDatabasePassword(){
			return config.getValue('databasePassword');
		}

	}

}