<?php
namespace integration\Message{

	use config\Settings;

	class MessageSenderFactoryImpl implements MessageSenderFactory{

		private $settings;

		public function __construct(Settings $settings){
			$this->settings = $settings;
		}

		public function getMessageSender(){
			return new EmailMessageSender();
		}

	}

}