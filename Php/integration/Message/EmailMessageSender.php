namespace integration\Message{

	class EmailMessageSender implements MessageSender{

		public function send($to, $subject, $body){
			$headers = array();
			$headers[] = "From: PokerBunch.com <noreply@pokerbunch.com>";
			$headers[] = "Content-Type: text/plain; charset=\"iso-8859-1\"";
			$headers[] = "X-Mailer: PHP/" . phpversion();
			return mail($to, $subject, $body, implode("\r\n", $headers));
		}

	}

}