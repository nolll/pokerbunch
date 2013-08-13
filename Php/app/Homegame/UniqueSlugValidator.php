namespace app\Homegame{

	use core\Validation\SimpleValidator;
	use Infrastructure\Data\Interfaces\HomegameStorage;

	class UniqueSlugValidator extends SimpleValidator {

		protected $homegameStorage;

		public function __construct($subject, $message, \Infrastructure\Data\Interfaces\HomegameStorage $homegameStorage){
			parent::__construct($subject, $message);
			$this->homegameStorage = $homegameStorage;
		}

		public function validateSubject(){
			$existingHomegame = $this->homegameStorage->getRawHomegameByName($this->subject);
			if($existingHomegame != null){
				$this->addError($this->message);
				return false;
			}
			return true;
		}

	}

}