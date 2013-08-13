namespace app\Cashgame\Add{

	use Mishiin\Request;

	class AddPostModel {

		public $location;

		public function __construct(Request $request){
			location = $request.getParamPost('location');
			if(location == null || location == ''){
				location = $request.getParamPost('location-dropdown');
			}
		}

	}

}