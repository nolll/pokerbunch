<?php
namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\HomegameUrlModel;
	use entities\Homegame;

	class CashgameEndUrlModel extends HomegameUrlModel{

		public function __construct(Homegame $homegame){
			parent::__construct(RouteFormats::cashgameEnd, $homegame);
		}

	}

}