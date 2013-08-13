<?php
namespace app\Cashgame\Add{

	use Mishiin\Request;

	class AddPostModel {

		public $location;

		public function __construct(Request $request){
			$this->location = $request->getParamPost('location');
			if($this->location == null || $this->location == ''){
				$this->location = $request->getParamPost('location-dropdown');
			}
		}

	}

}