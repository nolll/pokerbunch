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
			returnUrl = $returnUrl;
			if(returnUrl == null){
				returnUrl = '/';
			}
			addUserUrl = new UserAddUrlModel();
			forgotPasswordUrl = new ForgotPasswordUrlModel();
			if($loginName != null){
				loginName = $loginName;
			}
		}

	}

}