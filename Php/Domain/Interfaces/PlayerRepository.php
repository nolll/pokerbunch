<?php
namespace Domain\Interfaces {

	use entities\Homegame;
	use entities\Player;
	use Domain\Classes\User;

	interface PlayerRepository{

		/**
		 * @param Homegame $homegame
		 * @return Player[]
		 */
		public function getAll(Homegame $homegame);

		/**
		 * @param Homegame $homegame
		 * @param int $id
		 * @return Player
		 */
		public function getPlayerById(Homegame $homegame, $id);

		/**
		 * @param Homegame $homegame
		 * @param $name
		 * @return Player
		 */
		public function getByName(Homegame $homegame, $name);

		/**
		 * @param Homegame $homegame
		 * @param $userName
		 * @return Player
		 */
		public function getByUserName(Homegame $homegame, $userName);

		/**
		 * @param Homegame $homegame
		 * @param string $playerName
		 * @return int
		 */
		public function addPlayer(Homegame $homegame, $playerName);

		/**
		 * @param Homegame $homegame
		 * @param User $user
		 * @param $role
		 * @return int
		 */
		public function addPlayerWithUser(Homegame $homegame, User $user, $role);

		/**
		 * @param Player $player
		 * @param Homegame $homegame
		 * @param User $user
		 * @return bool
		 */
		public function joinHomegame(Player $player, Homegame $homegame, User $user);

		/**
		 * @param Player $player
		 * @return bool
		 */
		public function deletePlayer(Player $player);

	}

}