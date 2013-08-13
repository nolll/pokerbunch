<?php
namespace app\Cashgame\Add{

	use entities\CashgameFactory;
	use Mishiin\Request;
	use app\Urls\RunningCashgameUrlModel;
	use core\PageController;
	use core\UserContext;
	use entities\GameStatus;
	use core\DateTimeFactory;
	use core\Globalization;
	use exceptions\AccessDeniedException;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use entities\Cashgame;
	use app\Cashgame\CashgameValidatorFactory;
	use entities\Homegame;
	use app\Cashgame\Add\AddModel;

	class AddController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $cashgameValidatorFactory;
		private $request;
		private $cashgameFactory;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									CashgameValidatorFactory $cashgameValidatorFactory,
									Request $request,
									CashgameFactory $cashgameFactory){
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->cashgameValidatorFactory = $cashgameValidatorFactory;
			$this->request = $request;
			$this->cashgameFactory = $cashgameFactory;
		}

		public function action_add($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			if($runningGame != null){
				throw new AccessDeniedException('Game already running');
			}
			return $this->showForm($homegame);
		}

		public function action_add_post($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$postModel = new AddPostModel($this->request);
			return $this->handleAddPost($homegame, $postModel);
		}

        public function handleAddPost(Homegame $homegame, AddPostModel $postModel){
            $cashgame = $this->getCashgame($postModel);
            $validator = $this->cashgameValidatorFactory->getAddCashgameValidator($homegame, $cashgame);
            if($validator->isValid()){
                $this->cashgameRepository->addGame($homegame, $cashgame);
				return $this->redirect(new RunningCashgameUrlModel($homegame));
            } else {
                return $this->showForm($homegame, $cashgame, $validator->getErrors());
            }
        }

		private function getCashgame(AddPostModel $postModel){
			$cashgame = $this->cashgameFactory->create($postModel->location, GameStatus::running);
			return $cashgame;
		}

		public function showForm(Homegame $homegame, Cashgame $cashgame = null, array $validationErrors = null){
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$locations = $this->cashgameRepository->getLocations($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			$model = new AddModel($this->userContext->getUser(), $homegame, $cashgame, $locations, $years, $runningGame);
			if($validationErrors != null){
				$model->setValidationErrors($validationErrors);
			}
			return $this->view('app/Cashgame/Add/Add', $model);
		}

	}

}