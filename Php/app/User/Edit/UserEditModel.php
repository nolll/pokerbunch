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
			userName = $user.getUserName();
			displayName = $user.getDisplayName();
			realName = $user.getRealName();
			email = $user.getEmail();
		}

	}

}