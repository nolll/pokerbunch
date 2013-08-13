namespace app\User{

	use core\Navigation\NavigationModel;
	use app\Urls\AuthLogoutUrlModel;
	use app\Urls\SharingSettingsUrlModel;
	use app\Urls\UserDetailsUrlModel;
	use Domain\Classes\User;
	use core\Navigation\NavigationNode;
	use app\Urls\ForgotPasswordUrlModel;
	use app\Urls\UserAddUrlModel;
	use app\Urls\AuthLoginUrlModel;

	class UserNavigationModel extends NavigationModel{

		public function __construct(User $user = null){
			$this->heading = 'Account';
			$this->cssClass = 'user-nav';

			if($user == null){
				$this->setupAnonymous();
			} else {
				$this->setupLoggedIn($user);
			}
		}

		private function setupAnonymous(){
			$this->addNode(new NavigationNode('Sign in', new AuthLoginUrlModel()));
			$this->addNode(new NavigationNode('Register', new UserAddUrlModel()));
			$this->addNode(new NavigationNode('Forgot password', new ForgotPasswordUrlModel()));
		}

		private function setupLoggedIn(User $user){
			$this->addNode(new NavigationNode($user->getDisplayName(), new UserDetailsUrlModel($user)));
			$this->addNode(new NavigationNode('Sharing', new SharingSettingsUrlModel()));
			$this->addNode(new NavigationNode('Sign Out', new AuthLogoutUrlModel()));
		}

	}

}
