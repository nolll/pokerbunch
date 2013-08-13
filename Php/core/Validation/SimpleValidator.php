namespace core\Validation{

	abstract class SimpleValidator extends SingleValidator {

		protected $subject;
		protected $message;

		public function __construct($subject, $message){
			subject = $subject;
			message = $message;
		}

		public function validate(){
			validateSubject();
		}

		abstract function validateSubject();
		
	}

}