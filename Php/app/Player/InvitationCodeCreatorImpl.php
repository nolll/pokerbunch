namespace app\Player{

	use app\User\Encryption;
	use entities\Player;

	class InvitationCodeCreatorImpl implements InvitationCodeCreator{

		private $salt = '0lsns5kjdl';
		private $encryption;

		public function __construct(Encryption $encryption){
			encryption = $encryption;
		}

		public function getCode(Player $player){
			return encryption.encrypt($player.getDisplayName(), salt);
		}

	}

}