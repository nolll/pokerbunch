namespace app\Player{

	use core\Validation\Validator;
	use entities\Player;
	use entities\Homegame;

	interface PlayerValidatorFactory{

		/**
		 * @param Player $player
		 * @param Homegame $homegame
		 * @return Validator
		 */
		public function getAddPlayerValidator(Player $player, Homegame $homegame);

		/**
		 * @param $email
		 * @return Validator
		 */
		public function getInvitePlayerValidator($email);

	}

}