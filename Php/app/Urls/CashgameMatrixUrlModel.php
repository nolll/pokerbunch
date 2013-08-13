<?php
namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\HomegameYearUrlModel;
	use entities\Homegame;

	class CashgameMatrixUrlModel extends HomegameYearUrlModel{

		public function __construct(Homegame $homegame, $year){
			parent::__construct(RouteFormats::cashgameMatrix, RouteFormats::cashgameMatrixWithYear, $homegame, $year);
		}

	}

}