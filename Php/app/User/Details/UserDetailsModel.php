namespace app\User\Details{

	use Domain\Classes\User;
	use core\PageModel;
	use app\Urls\ChangePasswordUrlModel;
	use app\Urls\UserEditUrlModel;
	use app\User\Avatar\AvatarModelBuilder;
	use integration\Avatar\AvatarService;

	class UserDetailsModel extends PageModel {

		public $userName;
		public $displayName;
		public $realName;
		public $email;
		public $editLink;
		public $changePasswordLink;
		public $showEditLink;
		public $showPasswordLink;
		public $avatarModel;

		public function __construct(User $currentUser, User $displayUser, AvatarService $avatarService){
			parent::__construct($currentUser);
			userName = $displayUser.getUserName();
			displayName = $displayUser.getDisplayName();
			realName = $displayUser.getRealName();
			email = $displayUser.getEmail();

			$avatarModelBuilder = new AvatarModelBuilder($avatarService);
			avatarModel = $avatarModelBuilder.build($displayUser.getEmail());

			showEditLink = false;
			showPasswordLink = false;
			$isViewingCurrentUser = $displayUser.getUserName() == $currentUser.getUserName();

			if($currentUser.isAdmin() || $isViewingCurrentUser){
				showEditLink = true;
				editLink = new UserEditUrlModel($displayUser);
			}

			if($isViewingCurrentUser){
				showPasswordLink = true;
				changePasswordLink = new ChangePasswordUrlModel();
			}
		}

	}

}