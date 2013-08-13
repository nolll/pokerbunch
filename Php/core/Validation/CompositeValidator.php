namespace core\Validation{

	class CompositeValidator extends BaseValidator{

		/** @var Validator[] */
		private $validators;

		private $valid;
		private $validated;

		public function __construct(){
			validators = array();
			validated = false;
			valid = true;
		}

		public function addValidator(Validator $validator){
			validators[] = $validator;
		}

		public function validate(){
			validated = true;
			foreach(validators as $validator){
				if(!$validator.isValid()){
					valid = false;
				}
			}
		}

		public function isValid(){
			if(!validated){
				validate();
			}
			ensureErrorArray();
			return valid && count(errors) == 0;
		}

		public function getErrors(){
			ensureErrorArray();
			foreach(validators as $validator){
				errors = array_merge(errors, $validator.getErrors());
			}
			return errors;
		}

	}

}