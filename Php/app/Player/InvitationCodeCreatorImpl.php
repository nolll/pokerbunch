<?php
namespace app\Player{

	use app\User\Encryption;
	use entities\Player;

	class InvitationCodeCreatorImpl implements InvitationCodeCreator{

		private $salt = '0lsns5kjdl';
		private $encryption;

		public function __construct(Encryption $encryption){
			$this->encryption = $encryption;
		}

		public function getCode(Player $player){
			return $this->encryption->encrypt($player->getDisplayName(), $this->salt);
		}

	}

}