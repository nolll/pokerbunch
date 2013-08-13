namespace app\Admin{
	use app\Urls\UserListingUrlModel;
	use core\Navigation\NavigationNode;
	use app\Urls\HomegameListingUrlModel;
	use core\Navigation\NavigationModel;
	use Domain\Classes\User;

	class AdminNavModel extends NavigationModel {

		public function __construct(User $user = null){
			parent::__construct('Admin', null, 'admin-nav');
			$isAdmin = $user != null && $user->isAdmin();
			if($isAdmin){
				$selected = false;
				$this->addNode(new NavigationNode('Bunches', new HomegameListingUrlModel(), $selected));
				$this->addNode(new NavigationNode('Users', new UserListingUrlModel(), $selected));
			}
		}

	}

}