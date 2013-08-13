namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\PlayerUrlModel;
	use entities\Player;
	use entities\Homegame;

	class PlayerDeleteUrlModel extends PlayerUrlModel{

		public function __construct(Homegame $homegame, Player $player){
			parent::__construct(RouteFormats::playerDelete, $homegame, $player);
		}

	}

}