namespace app\Homegame\Add{

	use entities\CurrencySettings;
	use entities\Homegame;
	use Domain\Classes\User;
	use core\PageModel;
	use DateTimeZone;
	use core\FormFields\CurrencyLayoutFieldModel;
	use core\FormFields\TimezoneFieldModel;
	use core\Globalization;

	class HomegameAddModel extends PageModel {

		public $displayName;
		public $description;
		public $currencySymbol;
		public $currencyLayoutSelectModel;
		public $timezoneSelectModel;

		public function __construct(User $user, Homegame $homegame = null){
			parent::__construct($user);
			if($homegame != null){
				displayName = $homegame.getDisplayName();
				description = $homegame.getDescription();
				setTimezoneAndCurrency($homegame.getTimezone(), $homegame.getCurrency());
			} else {
				setTimezoneAndCurrency(Homegame::getDefaultTimezone(), Homegame::getDefaultCurrency());
			}
		}

		private function setTimezoneAndCurrency(DateTimeZone $timezone, CurrencySettings $currency){
			timezoneSelectModel = getTimezoneSelectModel($timezone);
			currencySymbol = $currency.getSymbol();
			currencyLayoutSelectModel = getCurrencyLayoutSelectModel($currency.getLayout());
		}

		private function getTimezoneSelectModel(DateTimeZone $timezone){
			$timezoneNames = Globalization::getTimezoneNames();
			return new TimezoneFieldModel('timezone', 'timezone', $timezone.getName(), $timezoneNames, 'Select Timezone');
		}

		private function getCurrencyLayoutSelectModel($layout){
			return new CurrencyLayoutFieldModel('currencylayout', 'currencylayout', $layout);
		}

	}

}