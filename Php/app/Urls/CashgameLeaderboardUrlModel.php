<?php
namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\HomegameYearUrlModel;
	use entities\Homegame;

	class CashgameLeaderboardUrlModel extends HomegameYearUrlModel{

		public function __construct(Homegame $homegame, $year){
			parent::__construct(RouteFormats::cashgameLeaderboard, RouteFormats::cashgameLeaderboardWithYear, $homegame, $year);
		}

	}

}