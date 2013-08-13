<?php
namespace app\Homegame\Listing{

	use core\PageController;
	use core\UserContext;
	use Infrastructure\Data\Interfaces\HomegameStorage;

	class HomegameListingController extends PageController {

		private $userContext;
		private $homegameStorage;

		public function __construct(UserContext $userContext,
									HomegameStorage $homegameStorage){
			$this->userContext = $userContext;
			$this->homegameStorage = $homegameStorage;
		}

		public function action_listing(){
			$this->userContext->requireAdmin();
			$homegames = $this->homegameStorage->getHomegames();
			$model = new HomegameListingModel($this->userContext->getUser(), $homegames);
			return $this->view('app/Homegame/Listing/Listing', $model);
		}

    }

}