<?php
namespace app\Player{

	use entities\Homegame;
	use entities\Player;

	interface InvitationSender{

		public function send(Homegame $homegame, Player $player, $email);

	}

}