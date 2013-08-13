<?php
namespace app\Player\Listing{

	use core\HomegamePageModel;
	use entities\Cashgame;
	use entities\Homegame;
	use Domain\Classes\User;
	use app\Urls\PlayerAddUrlModel;

	class PlayerListingModel extends HomegamePageModel {

		public $players;
		public $playerModels;
		public $addUrl;
		public $showAddLink;

		public function __construct(User $user,
									Homegame $homegame,
									array $players,
									$isInManagerMode,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->playerModels = $this->getPlayerModels($homegame, $players);
			$this->addUrl = new PlayerAddUrlModel($homegame);
			$this->showAddLink = $isInManagerMode;
		}

		private function getPlayerModels(Homegame $homegame, array $players){
			$models = array();
			foreach($players as $player){
				$models[] = new PlayerItemModel($homegame, $player);
			}
			return $models;
		}

	}

}