<?php
namespace app\User{

	class PasswordGenerator {

		private $passwordLength = 8;
		private $allowedCharacters = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-';

		public function createPassword(){
			$stringGenerator = new RandomStringGenerator($this->passwordLength, $this->allowedCharacters);
			return $stringGenerator->getString();
		}

	}

}