<?php
namespace app\Urls\BaseClasses{

	use entities\Player;
	use app\UrlFormatter;
	use entities\Homegame;

	class PlayerUrlModel extends UrlModel{

		public function __construct($format, Homegame $homegame, Player $player){
			$url = UrlFormatter::formatPlayer($format, $homegame, $player);
			parent::__construct($url);
		}

	}

}