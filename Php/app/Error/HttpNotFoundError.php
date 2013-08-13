namespace app\Error{

	class HttpNotFoundError extends HttpError{

		public function __construct(){
			parent::__construct(404, 'app/Error/NotFound');
		}

	}

}
