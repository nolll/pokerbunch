<?php
namespace Infrastructure\Data\Interfaces {

	use DateTime;
	use entities\GameStatus;
	use entities\Homegame;
	use entities\Checkpoints\Checkpoint;
	use entities\Cashgame;
	use entities\Player;
	use Infrastructure\Data\Classes\RawCashgame;
	use int;

	interface CashgameStorage{

		/**
		 * @param Homegame $homegame
		 * @param Cashgame $cashgame
		 * @return int
		 */
		public function addGame(Homegame $homegame, Cashgame $cashgame);

		/**
		 * @param Cashgame $cashgame
		 * @return bool
		 */
		public function deleteGame(Cashgame $cashgame);

		public function addCheckpoint(Cashgame $cashgame, Player $player, Checkpoint $checkpoint);

		public function updateCheckpoint(Checkpoint $checkpoint);

		public function deleteCheckpoint($id);

		/**
		 * @param Homegame $homegame
		 * @param DateTime $date
		 * @return RawCashgame
		 */
		public function getGame(Homegame $homegame, DateTime $date);

		/**
		 * @param Homegame $homegame
		 * @param GameStatus $status
		 * @param $year
		 * @return RawCashgame[]
		 */
		public function getGames(Homegame $homegame, $status = null, $year = null);

		/**
		 * @param Homegame $homegame
		 * @return int[]
		 */
		public function getYears(Homegame $homegame);

		/**
		 * @param RawCashgame $cashgame
		 * @return bool
		 */
		public function updateGame(RawCashgame $cashgame);

		public function hasPlayed(Player $player);

		public function getLocations(Homegame $homegame);

	}

}