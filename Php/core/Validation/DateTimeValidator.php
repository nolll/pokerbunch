namespace core\Validation{

	use Exception;
	use DateTime;

	class DateTimeValidator extends SimpleValidator {

		public function validateSubject(){
			if($this->isNullOrEmpty($this->subject)){
				return true;
			}
			try{
				new DateTime($this->subject);
				return true;
			} catch (Exception $e){
				$this->addError($this->message);
				return false;
			}
		}

	}

}