namespace core\Validation{

	class NotNullValidator extends SingleValidator {

		protected $subject;
		protected $message;

		public function __construct($subject = null, $message){
			$this->subject = $subject;
			$this->message = $message;
		}

		public function validate(){
			if($this->subject == null){
				$this->addError($this->message);
				return false;
			}
			return true;
		}

	}

}