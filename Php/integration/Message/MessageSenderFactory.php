namespace integration\Message{

	interface MessageSenderFactory{

		/**
		 * @return MessageSender
		 */
		public function getMessageSender();

	}

}