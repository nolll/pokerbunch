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
			userContext = $userContext;
			homegameRepository = $homegameRepository;
			matrixModelFactory = $matrixModelFactory;
		}

		public function action_matrix($gameName, $year = null){
			$homegame = homegameRepository.getByName($gameName);
			userContext.requirePlayer($homegame);
			$model = matrixModelFactory.get($homegame, userContext.getUser(), $year);
			return view('app/Cashgame/Matrix/Matrix', $model);
		}

	}

}