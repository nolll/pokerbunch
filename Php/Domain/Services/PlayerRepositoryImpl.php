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
			playerStorage = $playerStorage;
		}

		public function getAll(Homegame $homegame){
			return playerStorage.getPlayers($homegame);
		}

		public function getPlayerById(Homegame $homegame, $id){
			return playerStorage.getPlayerById($homegame, $id);
		}

		public function getByName(Homegame $homegame, $name){
			return playerStorage.getPlayerByName($homegame, $name);
		}

		public function getByUserName(Homegame $homegame, $userName){
			return playerStorage.getPlayerByUserName($homegame, $userName);
		}

		public function addPlayer(Homegame $homegame, $playerName){
			return playerStorage.addPlayer($homegame, $playerName);
		}

		public function addPlayerWithUser(Homegame $homegame, User $user, $role){
			return playerStorage.addPlayerWithUser($homegame, $user, $role);
		}

		public function joinHomegame(Player $player, Homegame $homegame, User $user){
			return playerStorage.joinHomegame($player, $homegame, $user);
		}

		public function deletePlayer(Player $player){
			return playerStorage.deletePlayer($player);
		}

	}

}