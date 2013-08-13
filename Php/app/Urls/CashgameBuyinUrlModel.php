namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\PlayerUrlModel;
	use entities\Player;
	use app\UrlFormatter;
	use entities\Homegame;

	class CashgameBuyinUrlModel extends PlayerUrlModel{

		public function __construct(Homegame $homegame, Player $player){
			parent::__construct(RouteFormats::cashgameBuyin, $homegame, $player);
		}

	}

}