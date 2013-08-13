<?php
namespace app\Urls{

	use app\RouteFormats;
	use app\Urls\BaseClasses\UrlModel;

	class HomegameListingUrlModel extends UrlModel{

		public function __construct(){
			parent::__construct(RouteFormats::homegameListing);
		}

	}

}