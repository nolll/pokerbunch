<?php
namespace app\User\Listing{
	use Domain\Classes\User;
	use app\Urls\UserDetailsUrlModel;

	class UserItemModel{

		public $name;
		public $urlModel;

		public function __construct(User $user){
			$this->name = $user->getDisplayName();
			$this->urlModel = new UserDetailsUrlModel($user);
		}

	}

}