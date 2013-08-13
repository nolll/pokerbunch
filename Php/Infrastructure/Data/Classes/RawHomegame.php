namespace Infrastructure\Data\Classes {

	class RawHomegame{

		private $id;
		private $slug;
		private $displayName;
		private $description;
		private $houseRules;
		private $timezoneName;
		private $defaultBuyin;
		private $currencyLayout;
		private $currencySymbol;

		/** @var bool */
		public $cashgamesEnabled;

		/** @var bool */
		public $tournamentsEnabled;

		/** @var bool */
		public $videosEnabled;

		public function getId(){
			return $this->id;
		}

		public function setId($id){
			$this->id = $id;
		}

		public function getSlug(){
			return $this->slug;
		}

		public function setSlug($name){
			$this->slug = $name;
		}

		public function getDisplayName(){
			return $this->displayName;
		}

		public function setDisplayName($displayName){
			$this->displayName = $displayName;
		}

		public function getDescription(){
			return $this->description;
		}

		public function setDescription($description){
			$this->description = $description;
		}

		public function getHouseRules(){
			return $this->houseRules;
		}

		public function setHouseRules($houseRules){
			$this->houseRules = $houseRules;
		}

		public function getTimezoneName(){
			return $this->timezoneName;
		}

		public function setTimezoneName($timezoneName){
			$this->timezoneName = $timezoneName;
		}

		public function getDefaultBuyin(){
			return $this->defaultBuyin;
		}

		public function setDefaultBuyin($defaultBuyin){
			$this->defaultBuyin = $defaultBuyin;
		}

		public function getCurrencyLayout(){
			return $this->currencyLayout;
		}

		public function setCurrencyLayout($currencyLayout){
			$this->currencyLayout = $currencyLayout;
		}

		public function getCurrencySymbol(){
			return $this->currencySymbol;
		}

		public function setCurrencySymbol($currencySymbol){
			$this->currencySymbol = $currencySymbol;
		}

	}

}
