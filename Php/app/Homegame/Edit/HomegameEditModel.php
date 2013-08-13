namespace app\Homegame\Edit{

	use core\HomegamePageModel;
	use entities\Cashgame;
	use entities\Homegame;
	use Domain\Classes\User;
	use DateTimeZone;
	use core\FormFields\CurrencyLayoutFieldModel;
	use core\FormFields\TimezoneFieldModel;
	use core\Globalization;
	use app\Urls\HomegameDetailsUrlModel;

	class HomegameEditModel extends HomegamePageModel {

		public $cancelUrl;
		public $heading;
		public $currencySymbol;
		public $currencyLayoutSelectModel;
		public $description;
		public $houseRules;
		public $timezoneSelectModel;
		public $defaultBuyin;
		public $cashgamesEnabled;
		public $tournamentsEnabled;
		public $videosEnabled;

		public function __construct(User $user,
									Homegame $homegame,
									Cashgame $runningGame = null){
			parent::__construct($user, $homegame, $runningGame);
			$this->cancelUrl = new HomegameDetailsUrlModel($homegame);
			$this->heading = $homegame->getDisplayName() . ' Settings';
			$currency = $homegame->getCurrency();
			$this->currencySymbol = $currency->getSymbol();
			$this->currencyLayoutSelectModel = $this->getCurrencyLayoutSelectModel($currency->getLayout());
			$this->description = $homegame->getDescription();
			$this->houseRules = $homegame->getHouseRules();
			$this->timezoneSelectModel = $this->getTimezoneSelectModel($homegame->getTimezone());
			$this->defaultBuyin = $homegame->getDefaultBuyin();
			$this->cashgamesEnabled = $homegame->cashgamesEnabled;
			$this->tournamentsEnabled = $homegame->tournamentsEnabled;
			$this->videosEnabled = $homegame->videosEnabled;
		}

		private function getTimezoneSelectModel(DateTimeZone $timezone){
			$timezoneNames = Globalization::getTimezoneNames();
			return new TimezoneFieldModel('timezone', 'timezone', $timezone->getName(), $timezoneNames);
		}

		private function getCurrencyLayoutSelectModel($layout){
			return new CurrencyLayoutFieldModel('currencylayout', 'currencylayout', $layout);
		}

	}

}