<?php
namespace core\Validation{

	abstract class SimpleValidator extends SingleValidator {

		protected $subject;
		protected $message;

		public function __construct($subject, $message){
			$this->subject = $subject;
			$this->message = $message;
		}

		public function validate(){
			$this->validateSubject();
		}

		abstract function validateSubject();
		
	}

}