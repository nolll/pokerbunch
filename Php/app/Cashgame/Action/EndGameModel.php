<?php
namespace app\Cashgame\Action{

	use core\Globalization;
	use core\HomegamePageModel;
	use entities\Cashgame;
	use Domain\Classes\User;
	use entities\Homegame;

    class EndGameModel extends HomegamePageModel {

		public $showDiff;

        public function __construct(User $user,
									Homegame $homegame,
									array $years = null,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->showDiff = true;
        }

	}

}