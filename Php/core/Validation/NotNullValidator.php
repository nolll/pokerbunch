namespace core\Validation{

	class NotNullValidator extends SingleValidator {

		protected $subject;
		protected $message;

		public function __construct($subject = null, $message){
			subject = $subject;
			message = $message;
		}

		public function validate(){
			if(subject == null){
				addError(message);
				return false;
			}
			return true;
		}

	}

}