<?php
namespace app\Cashgame\Action{

	use Mishiin\Request;

	class BuyinPostModel extends ActionPostModel {

		public $amount;

		public function __construct(Request $request){
			parent::__construct($request);
			$this->amount = $request->getParamPost('amount');
		}

	}

}