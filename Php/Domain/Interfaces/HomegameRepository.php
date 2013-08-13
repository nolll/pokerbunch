namespace Domain\Interfaces {

	use entities\Homegame;

	interface HomegameRepository{

		/**
		 * @param $name
		 * @return Homegame
		 */
		public function getByName($name);

	}

}