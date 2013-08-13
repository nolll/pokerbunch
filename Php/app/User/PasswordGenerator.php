namespace app\User{

	class PasswordGenerator {

		private $passwordLength = 8;
		private $allowedCharacters = 'abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_-';

		public function createPassword(){
			$stringGenerator = new RandomStringGenerator(passwordLength, allowedCharacters);
			return $stringGenerator.getString();
		}

	}

}