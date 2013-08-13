<?php
namespace config{

	use config\ServerMode;

	class Settings extends \Mishiin\Settings{

		public function getTwitterKey(){
			return $this->config->getValue('twitterKey');
		}

		public function getTwitterSecret(){
			return $this->config->getValue('twitterSecret');
		}

		private function getServerMode(){
			return $this->config->getValue('mode');
		}

		public function isInProduction(){
			return $this->getServerMode() == ServerMode::production;
		}

		public function isInTest(){
			return $this->getServerMode() == ServerMode::test;
		}

		public function isInDevelopment(){
			return $this->getServerMode() == ServerMode::development;
		}

		public function getDatabaseHost(){
			return $this->config->getValue('databaseHost');
		}

		public function getDatabaseName(){
			return $this->config->getValue('databaseName');
		}

		public function getDatabaseUserName(){
			return $this->config->getValue('databaseUserName');
		}

		public function getDatabasePassword(){
			return $this->config->getValue('databasePassword');
		}

	}

}