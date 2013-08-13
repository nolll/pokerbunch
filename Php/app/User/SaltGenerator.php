namespace app\User{

	class SaltGenerator {

		private $saltLength = 10;
		private $allowedCharacters = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-';

		public function createSalt(){
			$stringGenerator = new RandomStringGenerator($this->saltLength, $this->allowedCharacters);
			return $stringGenerator->getString();
		}

	}

}