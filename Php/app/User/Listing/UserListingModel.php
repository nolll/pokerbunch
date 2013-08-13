namespace app\User\Listing{

	use core\PageModel;
	use Domain\Classes\User;

	class UserListingModel extends PageModel {

		public $userModels;

		public function __construct(User $user, array $users){
			parent::__construct($user);
			userModels = getUserModels($users);
		}

		private function getUserModels($users){
			$models = array();
			foreach($users as $user){
				$models[] = new UserItemModel($user);
			}
			return $models;
		}

	}

}