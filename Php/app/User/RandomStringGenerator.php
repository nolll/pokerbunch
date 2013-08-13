namespace app\User{

	class RandomStringGenerator {

		private $stringLength;
		private $allowedCharacters;

		public function __construct($stringLength, $allowedCharacters){
			stringLength = $stringLength;
			allowedCharacters = $allowedCharacters;
		}

		public function getString(){
			$password = '';
			for($i = 0; $i < stringLength; $i++){
				$randomPos = rand(0, strlen(allowedCharacters) - 1);
				$password .= substr(allowedCharacters, $randomPos, 1);
			}
			return $password;
		}

	}

}