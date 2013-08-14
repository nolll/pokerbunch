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
			return id;
		}

		public function setId($id){
			id = $id;
		}

		public function getSlug(){
			return slug;
		}

		public function setSlug($name){
			slug = $name;
		}

		public function getDisplayName(){
			return displayName;
		}

		public function setDisplayName($displayName){
			displayName = $displayName;
		}

		public function getDescription(){
			return description;
		}

		public function setDescription($description){
			description = $description;
		}

		public function getHouseRules(){
			return houseRules;
		}

		public function setHouseRules($houseRules){
			houseRules = $houseRules;
		}

		public function getTimezoneName(){
			return timezoneName;
		}

		public function setTimezoneName($timezoneName){
			timezoneName = $timezoneName;
		}

		public function getDefaultBuyin(){
			return defaultBuyin;
		}

		public function setDefaultBuyin($defaultBuyin){
			defaultBuyin = $defaultBuyin;
		}

		public function getCurrencyLayout(){
			return currencyLayout;
		}

		public function setCurrencyLayout($currencyLayout){
			currencyLayout = $currencyLayout;
		}

		public function getCurrencySymbol(){
			return currencySymbol;
		}

		public function setCurrencySymbol($currencySymbol){
			currencySymbol = $currencySymbol;
		}

	}

}
