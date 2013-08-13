namespace app\Homegame\Join{

	use core\PageModel;
	use Domain\Classes\User;

	class HomegameJoinModel extends PageModel {

		public $code;

		public function __construct(User $user, $postedCode){
			parent::__construct($user);
			$this->code = $postedCode;
		}

	}

}