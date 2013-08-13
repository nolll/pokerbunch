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
			locations = array();
			setCurrency(self::getDefaultCurrency());
			setTimezone(self::getDefaultTimezone());
			defaultBuyin = 0;
			cashgamesEnabled = true;
			tournamentsEnabled = true;
			videosEnabled = false;
		}

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

		/**
		 * @return DateTimeZone
		 */
		public function getTimezone(){
			return timezone;
		}

		public function setTimezone(DateTimeZone $timezone = null){
			timezone = $timezone;
		}

		public function getDefaultBuyin(){
			return defaultBuyin;
		}

		public function setDefaultBuyin($defaultBuyin){
			defaultBuyin = $defaultBuyin;
		}

		/**
		 * @return CurrencySettings
		 */
		public function getCurrency(){
			return currency;
		}

		public function setCurrency(CurrencySettings $currency){
			currency = $currency;
		}

		public static function getDefaultTimezone(){
			return new DateTimeZone('UTC');
		}

		public static function getDefaultCurrency(){
			return new CurrencySettings('$', '{SYMBOL}{AMOUNT}');
		}

	}

}
