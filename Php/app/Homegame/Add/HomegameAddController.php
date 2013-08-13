<?php
namespace app\Homegame\Add{

	use Domain\Interfaces\PlayerRepository;
	use Mishiin\Request;
	use core\PageController;
	use core\PageModel;
	use DateTimeZone;
	use core\Globalization;
	use app\Urls\HomegameAddConfirmationUrlModel;
	use core\UserContext;
	use entities\CurrencySettings;
	use Infrastructure\Data\Interfaces\HomegameStorage;
	use entities\Homegame;
	use app\Homegame\SlugGenerator;
	use app\Homegame\HomegameValidatorFactory;
	use entities\Role;
	use app\Homegame\Add\HomegameAddModel;

	class HomegameAddController extends PageController {

		private $userContext;
		private $homegameStorage;
		private $playerRepository;
		private $slugGenerator;
		private $homegameValidatorFactory;
		private $request;

		public function __construct(UserContext $userContext,
									HomegameStorage $homegameStorage,
									PlayerRepository $playerRepository,
									SlugGenerator $slugGenerator,
									HomegameValidatorFactory $homegameValidatorFactory,
									Request $request){
			$this->userContext = $userContext;
			$this->homegameStorage = $homegameStorage;
			$this->playerRepository = $playerRepository;
			$this->slugGenerator = $slugGenerator;
			$this->homegameValidatorFactory = $homegameValidatorFactory;
			$this->request = $request;
		}

		public function action_add(){
			$this->userContext->requireUser();
			return $this->showForm();
		}

		public function action_add_post(){
			$this->userContext->requireUser();
			$homegame = $this->getPostedHomegame();
			$validator = $this->homegameValidatorFactory->getAddHomegameValidator($homegame);
			if($validator->isValid()){
				$homegame = $this->homegameStorage->addHomegame($homegame);
				$user = $this->userContext->getUser();
				$this->playerRepository->addPlayerWithUser($homegame, $user, Role::$manager);
				return $this->redirect(new HomegameAddConfirmationUrlModel());
			} else {
				return $this->showForm($homegame, $validator->getErrors());
			}
		}

		public function action_created(){
			$model = new PageModel($this->userContext->getUser());
			return $this->view('app/Homegame/Add/Confirmation', $model);
		}

		private function getPostedHomegame(){
			$homegame = new Homegame();
			$homegame->setDisplayName($this->request->getParamPost('displayname'));
			$currencySymbol = $this->request->getParamPost('currencysymbol');
			$currencyLayout = $this->request->getParamPost('currencylayout');
			$currency = new CurrencySettings($currencySymbol, $currencyLayout);
			$homegame->setCurrency($currency);
			$timezoneName = $this->request->getParamPost('timezone');
			if($timezoneName != null){
				$homegame->setTimezone(new DateTimeZone($timezoneName));
			}
			$homegame->setDescription($this->request->getParamPost('description'));
			$homegame->setSlug($this->slugGenerator->getSlug($homegame->getDisplayName()));
			return $homegame;
		}

		private function showForm(Homegame $homegame = null, array $validationErrors = null){
			$model = new HomegameAddModel($this->userContext->getUser(), $homegame);
			if($validationErrors != null){
				$model->setValidationErrors($validationErrors);
			}
			return $this->view('app/Homegame/Add/Add', $model);
		}

	}

}