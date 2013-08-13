<?php
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
			$this->userName = $displayUser->getUserName();
			$this->displayName = $displayUser->getDisplayName();
			$this->realName = $displayUser->getRealName();
			$this->email = $displayUser->getEmail();

			$avatarModelBuilder = new AvatarModelBuilder($avatarService);
			$this->avatarModel = $avatarModelBuilder->build($displayUser->getEmail());

			$this->showEditLink = false;
			$this->showPasswordLink = false;
			$isViewingCurrentUser = $displayUser->getUserName() == $currentUser->getUserName();

			if($currentUser->isAdmin() || $isViewingCurrentUser){
				$this->showEditLink = true;
				$this->editLink = new UserEditUrlModel($displayUser);
			}

			if($isViewingCurrentUser){
				$this->showPasswordLink = true;
				$this->changePasswordLink = new ChangePasswordUrlModel();
			}
		}

	}

}