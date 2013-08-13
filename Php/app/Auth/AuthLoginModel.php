<?php
namespace app\Auth{

	use app\Urls\ForgotPasswordUrlModel;
	use core\PageModel;
	use app\Urls\UserAddUrlModel;

	class AuthLoginModel extends PageModel {

		public $returnUrl;
		public $addUserUrl;
		public $forgotPasswordUrl;
		public $loginName;

		public function __construct($returnUrl = null, $loginName){
			parent::__construct();
			$this->returnUrl = $returnUrl;
			if($this->returnUrl == null){
				$this->returnUrl = '/';
			}
			$this->addUserUrl = new UserAddUrlModel();
			$this->forgotPasswordUrl = new ForgotPasswordUrlModel();
			if($loginName != null){
				$this->loginName = $loginName;
			}
		}

	}

}