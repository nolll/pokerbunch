<?php
namespace app\User\Listing{

	use core\PageController;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\UserStorage;

	class UserListingController extends PageController {

		private $userContext;
		private $userStorage;

		public function __construct(UserContext $userContext,
									UserStorage $userStorage){
			$this->userContext = $userContext;
			$this->userStorage = $userStorage;
		}

		public function action_listing(){
			$this->userContext->requireAdmin();
			$users = $this->userStorage->getUsers();
			$model = new UserListingModel($this->userContext->getUser(), $users);

			return $this->view('app/User/Listing/Listing', $model);
		}

	}

}