<?php
namespace app\Urls\BaseClasses{

	use app\UrlFormatter;
	use entities\Homegame;

	class HomegameYearUrlModel extends UrlModel{

		public function __construct($format, $formatWithYear, Homegame $homegame, $year){
			if($year != null){
				$this->url = UrlFormatter::formatHomegameWithYear($formatWithYear, $homegame, $year);
			} else {
				$this->url = UrlFormatter::formatHomegame($format, $homegame);
			}
		}

	}

}