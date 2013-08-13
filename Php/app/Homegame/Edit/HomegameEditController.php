<?php
namespace app\Homegame\Edit{

	use Domain\Interfaces\CashgameRepository;
	use Mishiin\Request;
	use core\PageController;
	use DateTimeZone;
	use core\Globalization;
	use app\Urls\HomegameDetailsUrlModel;
	use core\UserContext;
	use entities\CurrencySettings;
	use Infrastructure\Data\Interfaces\HomegameStorage;
	use Domain\Interfaces\HomegameRepository;
	use entities\Homegame;
	use app\Homegame\HomegameValidatorFactory;
	use app\Homegame\Edit\HomegameEditModel;

	class HomegameEditController extends PageController {

		private $userContext;
		private $homegameStorage;
		private $homegameRepository;
		private $homegameValidatorFactory;
		private $cashgameRepository;
		private $request;

		public function __construct(UserContext $userContext,
									HomegameStorage $homegameStorage,
									HomegameRepository $homegameRepository,
									HomegameValidatorFactory $homegameValidatorFactory,
									CashgameRepository $cashgameRepository,
									Request $request){
			$this->userContext = $userContext;
			$this->homegameStorage = $homegameStorage;
			$this->homegameRepository = $homegameRepository;
			$this->homegameValidatorFactory = $homegameValidatorFactory;
			$this->cashgameRepository = $cashgameRepository;
			$this->request = $request;
		}

		public function action_edit($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			return $this->showForm($homegame);
		}

		public function action_edit_post($gameName){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requireManager($homegame);
			$homegame = $this->getPostedHomegame($homegame);
			$validator = $this->homegameValidatorFactory->getEditHomegameValidator($homegame);
			if($validator->isValid()){
				$this->homegameStorage->updateHomegame($homegame);
				return $this->redirect(new HomegameDetailsUrlModel($homegame));
			} else {
				return $this->showForm($homegame, $validator->getErrors());
			}
		}

		private function getPostedHomegame(Homegame $homegame){
			$homegame->setDescription($this->request->getParamPost('description'));
			$homegame->setHouseRules($this->request->getParamPost('houserules'));
			$currencySymbol = $this->request->getParamPost('currencysymbol');
			$currencyLayout = $this->request->getParamPost('currencylayout');
			$currency = new CurrencySettings($currencySymbol, $currencyLayout);
			$homegame->setCurrency($currency);
			$timezoneName = $this->request->getParamPost('timezone');
			if($timezoneName != null){
				$homegame->setTimezone(new DateTimeZone($timezoneName));
			}
			$homegame->setDefaultBuyin($this->request->getParamPost('defaultbuyin'));
			$homegame->cashgamesEnabled = $this->request->getParamPost('cashgames') != null;
			$homegame->tournamentsEnabled = $this->request->getParamPost('tournaments') != null;
			$homegame->videosEnabled = $this->request->getParamPost('videos') != null;
			return $homegame;
		}

		private function showForm(Homegame $homegame, array $validationErrors = null){
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$model = new HomegameEditModel($this->userContext->getUser(), $homegame, $runningGame);
			if($validationErrors != null){
				$model->setValidationErrors($validationErrors);
			}
			return $this->view('app/Homegame/Edit/Edit', $model);
		}

	}

}