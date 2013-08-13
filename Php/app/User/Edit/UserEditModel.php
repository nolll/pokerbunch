namespace app\User\Edit{

	use core\PageModel;
	use Domain\Classes\User;

	class UserEditModel extends PageModel {

		public $userName;
		public $displayName;
		public $realName;
		public $email;

		public function __construct(User $currentUser, User $user){
			parent::__construct($currentUser);
			$this->userName = $user->getUserName();
			$this->displayName = $user->getDisplayName();
			$this->realName = $user->getRealName();
			$this->email = $user->getEmail();
		}

	}

}