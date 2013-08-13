namespace app\Urls{

	use app\RouteFormats;
	use entities\Player;
	use app\Urls\BaseClasses\CashgamePlayerUrlModel;
	use app\UrlFormatter;
	use entities\Cashgame;
	use entities\Homegame;

	class CashgameActionUrlModel extends CashgamePlayerUrlModel{

		public function __construct(Homegame $homegame, Cashgame $cashgame, Player $player){
			parent::__construct(RouteFormats::cashgameAction, $homegame, $cashgame, $player);
		}

	}

}