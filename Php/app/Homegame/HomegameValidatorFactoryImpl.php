<?php
namespace app\Homegame{

	use entities\Homegame;
	use core\Validation\CompositeValidator;
	use Infrastructure\Data\Interfaces\HomegameStorage;
	use core\Validation\RequiredValidator;

	class HomegameValidatorFactoryImpl implements HomegameValidatorFactory{

		private $homegameStorage;

		public function __construct(\Infrastructure\Data\Interfaces\HomegameStorage $homegameStorage){
			$this->homegameStorage = $homegameStorage;
		}

		public function getAddHomegameValidator(Homegame $homegame){
			$validator = new CompositeValidator();
			$validator = $this->buildHomegameValidator($validator, $homegame);
			$validator = $this->buildUniqueHomegameValidator($validator, $homegame);
			return $validator;
		}

		public function getEditHomegameValidator(Homegame $homegame){
			$validator = new CompositeValidator();
			$validator = $this->buildHomegameValidator($validator, $homegame);
			return $validator;
		}

		private function buildHomegameValidator(CompositeValidator $validator, Homegame $homegame){
			$validator->addValidator(new RequiredValidator($homegame->getDisplayName(), 'Display Name can\'t be empty'));
			$currency = $homegame->getCurrency();
			$validator->addValidator(new RequiredValidator($currency->getSymbol(), 'Currency Symbol can\'t be empty'));
			$validator->addValidator(new RequiredValidator($currency->getLayout(), 'Currency Layout can\'t be empty'));
			$validator->addValidator(new RequiredValidator($homegame->getTimezone(), 'Timezone can\'t be empty'));
			return $validator;
		}

		private function buildUniqueHomegameValidator(CompositeValidator $validator, Homegame $homegame){
			$validator->addValidator(new UniqueSlugValidator($homegame->getSlug(), 'The Homegame name is not available', $this->homegameStorage));
			return $validator;
		}

	}

}