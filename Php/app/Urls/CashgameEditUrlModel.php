<?php
namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\CashgameUrlModel;
	use entities\Cashgame;
	use entities\Homegame;

	class CashgameEditUrlModel extends CashgameUrlModel{

		public function __construct(Homegame $homegame, Cashgame $cashgame){
			parent::__construct(RouteFormats::cashgameEdit, $homegame, $cashgame);
		}

	}

}