<?php
namespace app\Urls\BaseClasses{

	class UrlModel{

		public $url;

		public function __construct($url){
			$this->url = $url;
		}

		public function getUrl(){
			return $this->url;
		}

	}

}