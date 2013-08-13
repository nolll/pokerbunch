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
			heading = 'Account';
			cssClass = 'user-nav';

			if($user == null){
				setupAnonymous();
			} else {
				setupLoggedIn($user);
			}
		}

		private function setupAnonymous(){
			addNode(new NavigationNode('Sign in', new AuthLoginUrlModel()));
			addNode(new NavigationNode('Register', new UserAddUrlModel()));
			addNode(new NavigationNode('Forgot password', new ForgotPasswordUrlModel()));
		}

		private function setupLoggedIn(User $user){
			addNode(new NavigationNode($user.getDisplayName(), new UserDetailsUrlModel($user)));
			addNode(new NavigationNode('Sharing', new SharingSettingsUrlModel()));
			addNode(new NavigationNode('Sign Out', new AuthLogoutUrlModel()));
		}

	}

}
