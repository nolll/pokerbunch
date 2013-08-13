<?php
namespace app\User{

	class RandomStringGenerator {

		private $stringLength;
		private $allowedCharacters;

		public function __construct($stringLength, $allowedCharacters){
			$this->stringLength = $stringLength;
			$this->allowedCharacters = $allowedCharacters;
		}

		public function getString(){
			$password = '';
			for($i = 0; $i < $this->stringLength; $i++){
				$randomPos = rand(0, strlen($this->allowedCharacters) - 1);
				$password .= substr($this->allowedCharacters, $randomPos, 1);
			}
			return $password;
		}

	}

}