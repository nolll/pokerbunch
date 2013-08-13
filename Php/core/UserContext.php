<?php
namespace core{

	use Domain\Classes\User;
	use entities\Homegame;

	interface UserContext{

		/**
		 * @return string
		 */
		public function getToken();

		/**
		 * @return User
		 */
		public function getUser();

		/**
		 * @return bool
		 */
		public function isLoggedIn();

		public function getRole(Homegame $game);

		public function isInRole(Homegame $game, $roleToCheck);

		public function isGuest(Homegame $game);

		public function isPlayer(Homegame $game);

		public function isManager(Homegame $game);

		public function isAdmin();

		public function requireUser();

		public function requireRole(Homegame $homegame, $role);

		public function requirePlayer(Homegame $homegame);

		public function requireManager(Homegame $homegame);

		public function requireAdmin();

	}

}