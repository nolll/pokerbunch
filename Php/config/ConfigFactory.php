namespace config{
	use Mishiin\ConfigDefault;

	class ConfigFactory extends \Mishiin\ConfigFactory{

		public function getConfig($key){
			if($key == 'pokerbunch.com'){
				return new ConfigProduction();
			}
			if($key == 'pokerbunch.lan' || $key == 'america'){
				return new ConfigDevelopment();
			}
			if($key == 'test.pokerbunch.com'){
				return new ConfigTest();
			}
			if($key == 'integrationtest'){
				return new ConfigDatabaseTest();
			}
			return new ConfigDefault();
		}

	}

}