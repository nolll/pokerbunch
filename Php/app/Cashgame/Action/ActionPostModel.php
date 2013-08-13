namespace app\Cashgame\Action{

	use Mishiin\Request;

	class ActionPostModel {

		public $stack;

		public function __construct(Request $request){
			stack = $request.getParamPost('stack');
		}

	}

}