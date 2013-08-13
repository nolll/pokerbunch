<?php
namespace app\Sharing\Index{

	use core\PageController;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\SharingStorage;
	use integration\Sharing\SocialServiceProvider;
	use app\Sharing\Index\SharingIndexModel;

	class SharingIndexController extends PageController {

		private $userContext;
		private $sharingStorage;

		public function __construct(UserContext $userContext,
									SharingStorage $sharingStorage){
			$this->userContext = $userContext;
			$this->sharingStorage = $sharingStorage;
		}

		public function action_index(){
			$this->userContext->requireUser();
			$user = $this->userContext->getUser();
			$isSharing = $this->sharingStorage->isSharing($user, SocialServiceProvider::twitter);
			$model = new SharingIndexModel($user, $isSharing);
			return $this->view('app/Sharing/Index/Index', $model);
		}

	}

}