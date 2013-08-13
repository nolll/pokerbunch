<?php
namespace app\Player\Add{

	use core\HomegamePageModel;
	use entities\Cashgame;
	use entities\Homegame;
	use Domain\Classes\User;

	class PlayerAddConfirmationModel extends HomegamePageModel {

		public $homegameName;

		public function __construct(User $user,
									Homegame $homegame,
									Cashgame $runningGame){
			parent::__construct($user, $homegame, $runningGame);
			$this->homegameName = $homegame->getDisplayName();
		}

	}

}