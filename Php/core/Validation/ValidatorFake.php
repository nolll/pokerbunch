<?php
namespace core\Validation{

	class ValidatorFake implements Validator{

		private $isValid;
		private $errors;

		public function __construct($isValid = false){
			$this->isValid = $isValid;
			$this->errors = array();
		}

		public function isValid(){
			return $this->isValid;
		}

		public function setIsValid($isValid){
			$this->isValid = $isValid;
		}

		public function getErrors(){
			return $this->errors;
		}

		public function setErrors($errors){
			$this->errors = $errors;
		}

	}

}