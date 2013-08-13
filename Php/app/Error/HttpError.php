namespace app\Error{

	class HttpError{

		public $code;
		public $view;

		public function __construct($code, $view){
			$this->code = $code;
			$this->view = $view;
		}

	}

}
