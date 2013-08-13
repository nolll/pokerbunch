namespace core{

	use entities\Homegame;
	use Mishiin\Request;

	class WebContextImpl implements WebContext{

		private $request;

		public function __construct(Request $request){
			request = $request;
		}

		public function getCookie($name){
			return request.getCookie($name);
		}

	}

}