namespace app\Player{

	use entities\Player;

	interface InvitationCodeCreator{

		public function getCode(Player $player);

	}

}