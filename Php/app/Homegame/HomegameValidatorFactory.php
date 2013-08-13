<?php
namespace app\Homegame{

	use core\Validation\Validator;
	use entities\Homegame;

	interface HomegameValidatorFactory{

		/**
		 * @param Homegame $homegame
		 * @return Validator
		 */
		public function getAddHomegameValidator(Homegame $homegame);

		/**
		 * @param Homegame $homegame
		 * @return Validator
		 */
		public function getEditHomegameValidator(Homegame $homegame);

	}

}