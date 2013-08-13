namespace Domain\Services {

	use core\DateTimeFactory;
	use entities\Homegame;
	use entities\GameStatus;
	use Domain\Interfaces\PlayerRepository;
	use entities\Player;
	use Domain\Classes\User;
	use Infrastructure\Data\Interfaces\PlayerStorage;

	class PlayerRepositoryImpl implements PlayerRepository{

		private $playerStorage;

		public function __construct(PlayerStorage $playerStorage){
			$this->playerStorage = $playerStorage;
		}

		public function getAll(Homegame $homegame){
			return $this->playerStorage->getPlayers($homegame);
		}

		public function getPlayerById(Homegame $homegame, $id){
			return $this->playerStorage->getPlayerById($homegame, $id);
		}

		public function getByName(Homegame $homegame, $name){
			return $this->playerStorage->getPlayerByName($homegame, $name);
		}

		public function getByUserName(Homegame $homegame, $userName){
			return $this->playerStorage->getPlayerByUserName($homegame, $userName);
		}

		public function addPlayer(Homegame $homegame, $playerName){
			return $this->playerStorage->addPlayer($homegame, $playerName);
		}

		public function addPlayerWithUser(Homegame $homegame, User $user, $role){
			return $this->playerStorage->addPlayerWithUser($homegame, $user, $role);
		}

		public function joinHomegame(Player $player, Homegame $homegame, User $user){
			return $this->playerStorage->joinHomegame($player, $homegame, $user);
		}

		public function deletePlayer(Player $player){
			return $this->playerStorage->deletePlayer($player);
		}

	}

}