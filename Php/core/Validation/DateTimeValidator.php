namespace core\Validation{

	use Exception;
	use DateTime;

	class DateTimeValidator extends SimpleValidator {

		public function validateSubject(){
			if(isNullOrEmpty(subject)){
				return true;
			}
			try{
				new DateTime(subject);
				return true;
			} catch (Exception $e){
				addError(message);
				return false;
			}
		}

	}

}