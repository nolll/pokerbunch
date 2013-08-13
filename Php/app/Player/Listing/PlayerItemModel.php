namespace app\Player\Listing{

	use app\Urls\PlayerDetailsUrlModel;
	use entities\Homegame;
	use entities\Player;

	class PlayerItemModel{

		public $name;
		public $urlModel;
		public $email;

		public function __construct(Homegame $homegame, Player $player){
			$this->name = $player->getDisplayName();
			$this->urlModel = new PlayerDetailsUrlModel($homegame, $player);
		}

	}

}