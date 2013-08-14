namespace Infrastructure\Data\Interfaces {

	use entities\Homegame;
	use Domain\Classes\User;
	use Infrastructure\Data\Classes\RawHomegame;

	interface HomegameStorage{

		/**
		 * @return Homegame[]
		 */
		public function getHomegames();

		/**
		 * @param string $token
		 * @param int $role
		 * @return Homegame[]
		 */
		public function getHomegamesByRole($token, $role);

		/**
		 * @param Homegame $homegame
		 * @param User $user
		 * @return int
		 */
		public function getHomegameRole(Homegame $homegame, User $user);

		/**
		 * @param string $homegameName
		 * @return Homegame
		 */
		public function getHomegameByName($homegameName);

		/**
		 * @param string $homegameName
		 * @return RawHomegame
		 */
		public function getRawHomegameByName($homegameName);

		/**
		 * @param Homegame $homegame
		 * @return Homegame
		 */
		public function addHomegame(Homegame $homegame);

		/**
		 * @param Homegame $homegame
		 * @return bool
		 */
		public function updateHomegame(Homegame $homegame);

		/**
		 * @param Homegame $homegame
		 * @return bool
		 */
		public function deleteHomegame(Homegame $homegame);

	}

}