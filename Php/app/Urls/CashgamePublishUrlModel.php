<?php
namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\CashgameUrlModel;
	use entities\Cashgame;
	use entities\Homegame;

	class CashgamePublishUrlModel extends CashgameUrlModel{

		public function __construct(Homegame $homegame, Cashgame $cashgame){
			parent::__construct(RouteFormats::cashgamePublish, $homegame, $cashgame);
		}

	}

}