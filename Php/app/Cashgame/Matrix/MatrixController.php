namespace app\Cashgame\Matrix{

	use core\PageController;
	use Domain\Interfaces\HomegameRepository;
	use core\UserContext;

	class MatrixController extends PageController {

		private $userContext;
		private $homegameRepository;
		private $matrixModelFactory;

		public function __construct(UserContext $userContext,
									HomegameRepository $homegameRepository,
									MatrixModelFactory $matrixModelFactory){
			$this->userContext = $userContext;
			$this->homegameRepository = $homegameRepository;
			$this->matrixModelFactory = $matrixModelFactory;
		}

		public function action_matrix($gameName, $year = null){
			$homegame = $this->homegameRepository->getByName($gameName);
			$this->userContext->requirePlayer($homegame);
			$model = $this->matrixModelFactory->get($homegame, $this->userContext->getUser(), $year);
			return $this->view('app/Cashgame/Matrix/Matrix', $model);
		}

	}

}