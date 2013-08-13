<?php
namespace app\Cashgame\Edit{

	use core\DateTimeFactory;
	use app\Urls\CashgameListingUrlModel;
	use app\Urls\CashgameDetailsUrlModel;
	use core\Globalization;
	use Domain\Interfaces\CashgameRepository;
	use Domain\Interfaces\HomegameRepository;
	use app\Cashgame\CashgameValidatorFactory;
	use core\PageController;
	use core\UserContext;

	class CashgameEditController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $cashgameRepository;
		private $cashgameValidatorFactory;
		private $editPostModel;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									CashgameRepository $cashgameRepository,
									CashgameValidatorFactory $cashgameValidatorFactory,
									CashgameEditPostModel $editPostModel){
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->cashgameRepository = $cashgameRepository;
			$this->cashgameValidatorFactory = $cashgameValidatorFactory;
			$this->editPostModel = $editPostModel;
		}

		public function action_edit($gameName, $dateStr){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame->getTimezone());
			$cashgame = $this->cashgameRepository->getByDate($homegame, $date);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$locations = $this->cashgameRepository->getLocations($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			$model = new CashgameEditModel($this->userContext->getUser(), $homegame, $cashgame, $locations, $years, $runningGame, null);
			return $this->showForm($model);
		}

		private function showForm(CashgameEditModel $model){
			return $this->view('app/Cashgame/Edit/Edit', $model);
		}

		public function action_edit_post($gameName, $dateStr){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame->getTimezone());
			$cashgame = $this->cashgameRepository->getByDate($homegame, $date);
			$validator = $this->cashgameValidatorFactory->getEditCashgameValidator($this->editPostModel);
			if($validator->isValid()){
				$cashgame = $this->editPostModel->getCashgame($cashgame);
				$this->cashgameRepository->updateGame($cashgame);
				$detailsUrl = new CashgameDetailsUrlModel($homegame, $cashgame);
				return $this->redirect($detailsUrl);
			}
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$locations = $this->cashgameRepository->getLocations($homegame);
			$model = new CashgameEditModel($this->userContext->getUser(), $homegame, $cashgame, $locations, null, $runningGame, $this->editPostModel);
			$model->setValidationErrors($validator->getErrors());
			return $this->showForm($model);
		}

		public function action_delete($gameName, $dateStr){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			$date = DateTimeFactory::create($dateStr, $homegame->getTimezone());
			$cashgame = $this->cashgameRepository->getByDate($homegame, $date);
			$this->cashgameRepository->deleteGame($cashgame);
			$year = $date->format('Y');
			$listUrl = new CashgameListingUrlModel($homegame, $year);
			return $this->redirect($listUrl);
		}

	}

}