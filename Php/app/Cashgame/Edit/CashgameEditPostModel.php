namespace app\Cashgame\Edit{

	use entities\CashgameFactory;
	use Mishiin\Request;
	use core\DateTimeFactory;
	use entities\Cashgame;

	class CashgameEditPostModel {

		private $cashgameFactory;

		public $location;

		public function __construct(CashgameFactory $cashgameFactory,
									Request $request = null){
			$this->cashgameFactory = $cashgameFactory;

			if($request != null){
				$this->location = $request->getParamPost('location');
			}
		}

		public function getCashgame(Cashgame $cashgame){
			return $this->cashgameFactory->create($this->location, $cashgame->getStatus(), $cashgame->getId(), $cashgame->getResults());
		}

	}

}