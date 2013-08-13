namespace app\Cashgame\Action{

	use Mishiin\Request;

	class BuyinPostModel extends ActionPostModel {

		public $amount;

		public function __construct(Request $request){
			parent::__construct($request);
			amount = $request.getParamPost('amount');
		}

	}

}