<?php
namespace app\Homegame\Details{

	use core\PageController;
	use core\UserContext;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use entities\Homegame;
    use entities\Role;
	use app\Homegame\Details\HomegameDetailsModel;

	class HomegameDetailsController extends PageController {

		private $userContext;
		private $cashgameRepository;
		private $homegameRepository;

		public function __construct(UserContext $userContext,
									CashgameRepository $cashgameRepository,
									HomegameRepository $homegameRepository){
			$this->userContext = $userContext;
			$this->cashgameRepository = $cashgameRepository;
			$this->homegameRepository = $homegameRepository;
		}

		public function action_details($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$isInManagerMode = $this->userContext->isInRole($homegame, Role::$manager);
			$model = new HomegameDetailsModel($this->userContext->getUser(), $homegame, $isInManagerMode);
			return $this->view('app/Homegame/Details/Details', $model);
		}

	}

}