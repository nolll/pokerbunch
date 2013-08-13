<?php
namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\HomegameYearUrlModel;
	use entities\Homegame;

	class CashgameChartUrlModel extends HomegameYearUrlModel{

		public function __construct(Homegame $homegame, $year){
			parent::__construct(RouteFormats::cashgameChart, RouteFormats::cashgameChartWithYear, $homegame, $year);
		}

	}

}