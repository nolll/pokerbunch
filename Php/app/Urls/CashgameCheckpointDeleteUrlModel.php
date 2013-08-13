<?php
namespace app\Urls{

	use app\UrlFormatter;
	use app\Urls\BaseClasses\UrlModel;
	use app\RouteFormats;
	use entities\Checkpoints\Checkpoint;
	use entities\Cashgame;
	use entities\Homegame;
	use entities\Player;

	class CashgameCheckpointDeleteUrlModel extends UrlModel{

		public function __construct(Homegame $homegame, Cashgame $cashgame, Player $player, Checkpoint $checkpoint){
			$isoDate = UrlFormatter::formatIsoDate($cashgame->getStartTime());
			$encodedPlayerName = rawurlencode($player->getDisplayName());
			$url = sprintf(RouteFormats::cashgameCheckpointDelete, $homegame->getSlug(), $isoDate, $encodedPlayerName, $checkpoint->getId());
			parent::__construct($url);
		}

	}

}