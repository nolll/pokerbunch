namespace config{
	use config\ServerMode;

	class IntegrationTestSettings extends Settings{

		protected function getSectionKey(){
			return ServerMode::integration;
		}

	}

}