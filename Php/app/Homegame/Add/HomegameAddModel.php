<?php
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
				$this->displayName = $homegame->getDisplayName();
				$this->description = $homegame->getDescription();
				$this->setTimezoneAndCurrency($homegame->getTimezone(), $homegame->getCurrency());
			} else {
				$this->setTimezoneAndCurrency(Homegame::getDefaultTimezone(), Homegame::getDefaultCurrency());
			}
		}

		private function setTimezoneAndCurrency(DateTimeZone $timezone, CurrencySettings $currency){
			$this->timezoneSelectModel = $this->getTimezoneSelectModel($timezone);
			$this->currencySymbol = $currency->getSymbol();
			$this->currencyLayoutSelectModel = $this->getCurrencyLayoutSelectModel($currency->getLayout());
		}

		private function getTimezoneSelectModel(DateTimeZone $timezone){
			$timezoneNames = Globalization::getTimezoneNames();
			return new TimezoneFieldModel('timezone', 'timezone', $timezone->getName(), $timezoneNames, 'Select Timezone');
		}

		private function getCurrencyLayoutSelectModel($layout){
			return new CurrencyLayoutFieldModel('currencylayout', 'currencylayout', $layout);
		}

	}

}