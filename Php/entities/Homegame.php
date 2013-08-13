namespace entities{

	use DateTimeZone;

	class Homegame{

		private $id;
		private $slug;
		private $displayName;
		private $description;
		private $houseRules;
		private $timezone;
		private $defaultBuyin;
		private $currency;

		/** @var bool */
		public $cashgamesEnabled;

		/** @var bool */
		public $tournamentsEnabled;

		/** @var bool */
		public $videosEnabled;

		public function __construct(){
			$this->locations = array();
			$this->setCurrency(self::getDefaultCurrency());
			$this->setTimezone(self::getDefaultTimezone());
			$this->defaultBuyin = 0;
			$this->cashgamesEnabled = true;
			$this->tournamentsEnabled = true;
			$this->videosEnabled = false;
		}

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

		/**
		 * @return DateTimeZone
		 */
		public function getTimezone(){
			return $this->timezone;
		}

		public function setTimezone(DateTimeZone $timezone = null){
			$this->timezone = $timezone;
		}

		public function getDefaultBuyin(){
			return $this->defaultBuyin;
		}

		public function setDefaultBuyin($defaultBuyin){
			$this->defaultBuyin = $defaultBuyin;
		}

		/**
		 * @return CurrencySettings
		 */
		public function getCurrency(){
			return $this->currency;
		}

		public function setCurrency(CurrencySettings $currency){
			$this->currency = $currency;
		}

		public static function getDefaultTimezone(){
			return new DateTimeZone('UTC');
		}

		public static function getDefaultCurrency(){
			return new CurrencySettings('$', '{SYMBOL}{AMOUNT}');
		}

	}

}
