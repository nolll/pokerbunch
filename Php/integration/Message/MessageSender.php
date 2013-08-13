namespace integration\Message{

	interface MessageSender{

		public function send($to, $subject, $body);

	}

}