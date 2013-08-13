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
			cancelUrl = new HomegameDetailsUrlModel($homegame);
			heading = $homegame.getDisplayName() . ' Settings';
			$currency = $homegame.getCurrency();
			currencySymbol = $currency.getSymbol();
			currencyLayoutSelectModel = getCurrencyLayoutSelectModel($currency.getLayout());
			description = $homegame.getDescription();
			houseRules = $homegame.getHouseRules();
			timezoneSelectModel = getTimezoneSelectModel($homegame.getTimezone());
			defaultBuyin = $homegame.getDefaultBuyin();
			cashgamesEnabled = $homegame.cashgamesEnabled;
			tournamentsEnabled = $homegame.tournamentsEnabled;
			videosEnabled = $homegame.videosEnabled;
		}

		private function getTimezoneSelectModel(DateTimeZone $timezone){
			$timezoneNames = Globalization::getTimezoneNames();
			return new TimezoneFieldModel('timezone', 'timezone', $timezone.getName(), $timezoneNames);
		}

		private function getCurrencyLayoutSelectModel($layout){
			return new CurrencyLayoutFieldModel('currencylayout', 'currencylayout', $layout);
		}

	}

}