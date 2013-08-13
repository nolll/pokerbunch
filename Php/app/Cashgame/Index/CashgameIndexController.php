<?php
namespace app\Cashgame\Index{

	use Domain\Interfaces\CashgameRepository;
	use core\PageController;
	use app\Urls\CashgameAddUrlModel;
	use app\Urls\CashgameMatrixUrlModel;
	use Domain\Interfaces\HomegameRepository;
	use core\UserContext;

	class CashgameIndexController extends PageController {

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

		public function action_index($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			if(count($years) > 0){
				$year = $years[0];
				return $this->redirect(new CashgameMatrixUrlModel($homegame, $year));
			} else {
				return $this->redirect(new CashgameAddUrlModel($homegame));
			}
		}

	}

}