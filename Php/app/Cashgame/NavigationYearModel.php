<?php
namespace app\Cashgame{

	use app\Urls\BaseClasses\UrlModel;
	use entities\Homegame;

	class NavigationYearModel{

		public $link;
		public $text;

		public function __construct(UrlModel $link, $text){
			$this->link = $link;
			$this->text = $text;
		}

	}

}
