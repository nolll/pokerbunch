namespace core{

	use entities\Homegame;
	use Mishiin\Request;

	class WebContextImpl implements WebContext{

		private $request;

		public function __construct(Request $request){
			$this->request = $request;
		}

		public function getCookie($name){
			return $this->request->getCookie($name);
		}

	}

}