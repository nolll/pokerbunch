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
			cashgameFactory = $cashgameFactory;

			if($request != null){
				location = $request.getParamPost('location');
			}
		}

		public function getCashgame(Cashgame $cashgame){
			return cashgameFactory.create(location, $cashgame.getStatus(), $cashgame.getId(), $cashgame.getResults());
		}

	}

}