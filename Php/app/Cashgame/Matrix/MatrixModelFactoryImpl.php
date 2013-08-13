namespace app\Cashgame\Matrix{

	use Domain\Interfaces\CashgameRepository;
	use entities\Homegame;
	use Domain\Classes\User;

	class MatrixModelFactoryImpl implements MatrixModelFactory {

		private $cashgameRepository;

		public function __construct(CashgameRepository $cashgameRepository){
			$this->cashgameRepository = $cashgameRepository;
		}

		public function get(Homegame $homegame, User $user, $year = null){
			$suite = $this->cashgameRepository->getSuite($homegame, $year);
			$runningGame = $this->cashgameRepository->getRunning($homegame);
			$years = $this->cashgameRepository->getYears($homegame);
			return new MatrixModel($user, $homegame, $suite, $years, $year, $runningGame);
		}

	}

}