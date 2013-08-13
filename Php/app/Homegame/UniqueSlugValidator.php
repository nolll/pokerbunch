namespace app\Homegame{

	use core\Validation\SimpleValidator;
	use Infrastructure\Data\Interfaces\HomegameStorage;

	class UniqueSlugValidator extends SimpleValidator {

		protected $homegameStorage;

		public function __construct($subject, $message, \Infrastructure\Data\Interfaces\HomegameStorage $homegameStorage){
			parent::__construct($subject, $message);
			homegameStorage = $homegameStorage;
		}

		public function validateSubject(){
			$existingHomegame = homegameStorage.getRawHomegameByName(subject);
			if($existingHomegame != null){
				addError(message);
				return false;
			}
			return true;
		}

	}

}