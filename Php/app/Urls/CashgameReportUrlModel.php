namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\PlayerUrlModel;
	use entities\Player;
	use app\UrlFormatter;
	use entities\Homegame;

	class CashgameReportUrlModel extends PlayerUrlModel{

		public function __construct(Homegame $homegame, Player $player){
			parent::__construct(RouteFormats::cashgameReport, $homegame, $player);
		}

	}

}