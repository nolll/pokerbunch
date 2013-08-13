namespace app\Urls\BaseClasses{

	use app\UrlFormatter;
	use entities\Player;
	use entities\Cashgame;
	use entities\Homegame;

	class CashgamePlayerUrlModel extends UrlModel{

		public function __construct($format, Homegame $homegame, Cashgame $cashgame, Player $player){
			$isoDate = UrlFormatter::formatIsoDate($cashgame.getStartTime());
			$encodedPlayerName = rawurlencode($player.getDisplayName());
			$url = sprintf($format, $homegame.getSlug(), $isoDate, $encodedPlayerName);
			parent::__construct($url);
		}

	}

}