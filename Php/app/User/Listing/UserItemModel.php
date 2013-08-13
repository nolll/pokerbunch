namespace app\User\Listing{
	use Domain\Classes\User;
	use app\Urls\UserDetailsUrlModel;

	class UserItemModel{

		public $name;
		public $urlModel;

		public function __construct(User $user){
			name = $user.getDisplayName();
			urlModel = new UserDetailsUrlModel($user);
		}

	}

}