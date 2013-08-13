using Core.Repositories;

namespace Infrastructure.Repositories {
	
	public class CashgameRepositoryImpl : CashgameRepository{

        /*
	    public CashgameRepositoryImpl()
	    {
	        
	    }

		public function __construct(CashgameStorage $cashgameStorage,
									CashgameFactory $cashgameFactory,
									PlayerStorage $playerStorage,
									Timer $timer,
									CashgameSuiteFactory $cashgameSuiteFactory,
									CashgameResultFactory $cashgameResultFactory){
			$this->cashgameStorage = $cashgameStorage;
			$this->playerStorage = $playerStorage;
			$this->cashgameFactory = $cashgameFactory;
			$this->timer = $timer;
			$this->cashgameSuiteFactory = $cashgameSuiteFactory;
			$this->cashgameResultFactory = $cashgameResultFactory;
		}

		public function getPublished(Homegame $homegame, $year = null){
			return $this->getGames($homegame, GameStatus::published, $year);
		}

		public function getRunning(Homegame $homegame){
			$games = $this->getGames($homegame, GameStatus::running, null);
			if(count($games) == 0){
				return null;
			}
			return $games[0];
		}

		public function getAll(Homegame $homegame, $year = null){
			return $this->getGames($homegame, null, $year);
		}

		public function getByDate(Homegame $homegame, DateTime $date){
			$rawGame = $this->cashgameStorage->getGame($homegame, $date);
			$players = $this->playerStorage->getPlayers($homegame);
			return $this->getGameFromRawGame($rawGame, $players);
		}

		public function getByDateString(Homegame $homegame, $dateString){
			$date = DateTimeFactory::create($dateString, $homegame->getTimezone());
			return $this->getByDate($homegame, $date);
		}

		public function getSuite(Homegame $homegame, $year = null){
			$players = $this->playerStorage->getPlayers($homegame);
			$cashgames = $this->getPublished($homegame, $year);
			return $this->cashgameSuiteFactory->create($cashgames, $players);
		}

		public function getYears(Homegame $homegame){
			return $this->cashgameStorage->getYears($homegame);
		}

		private function getGames(Homegame $homegame, $status = null, $year = null){
			$rawGames = $this->cashgameStorage->getGames($homegame, $status, $year);
			$players = $this->playerStorage->getPlayers($homegame);
			return $this->getGamesFromRawGames($rawGames, $players);
		}

		private function getGamesFromRawGames(array $rawGames, array $players){
			$games = array();
			foreach($rawGames as $rawGame){
				$games[] = $this->getGameFromRawGame($rawGame, $players);
			}
			return $games;
		}

		private function getGameFromRawGame(RawCashgame $rawGame, array $players){
			$results = array();
			$rawResults = $rawGame->getResults();
			foreach($rawResults as $rawResult){
				$results[] = $this->getResultFromRawResult($rawResult, $players);
			}
			return $this->cashgameFactory->create($rawGame->getLocation(), $rawGame->getStatus(), $rawGame->getId(), $results);
		}

		private function getResultFromRawResult(RawCashgameResult $rawResult, array $players){
			$player = $this->getPlayer($players, $rawResult->getPlayerId());
			$checkpoints = $rawResult->getCheckpoints();
			return $this->cashgameResultFactory->create($player, $checkpoints);
		}

		private function getPlayer(array $players, $playerId){
			foreach($players as $player){
				if($player->getId() == $playerId){
					return $player;
				}
			}
			return null;
		}

		public function getLocations(Homegame $homegame){
			return $this->cashgameStorage->getLocations($homegame);
		}

		public function deleteGame(Cashgame $cashgame){
			return $this->cashgameStorage->deleteGame($cashgame);
		}

		public function addGame(Homegame $homegame, Cashgame $cashgame){
			return $this->cashgameStorage->addGame($homegame, $cashgame);
		}

		public function addCheckpoint(Cashgame $cashgame, Player $player, Checkpoint $checkpoint){
			$this->cashgameStorage->addCheckpoint($cashgame, $player, $checkpoint);
		}

		public function updateCheckpoint(Checkpoint $checkpoint){
			$this->cashgameStorage->updateCheckpoint($checkpoint);
		}

		public function deleteCheckpoint($id){
			$this->cashgameStorage->deleteCheckpoint($id);
		}

		public function updateGame(Cashgame $cashgame){
			$rawCashgame = $this->getRawCashgame($cashgame);
			return $this->cashgameStorage->updateGame($rawCashgame);
		}

		public function startGame(Cashgame $cashgame){
			$rawCashgame = $this->getRawCashgame($cashgame, GameStatus::running);
			return $this->cashgameStorage->updateGame($rawCashgame);
		}

		public function endGame(Cashgame $cashgame){
			$rawCashgame = $this->getRawCashgame($cashgame, GameStatus::published);
			return $this->cashgameStorage->updateGame($rawCashgame);
		}

		public function hasPlayed(Player $player){
			return $this->cashgameStorage->hasPlayed($player);
		}

		private function getRawCashgame(Cashgame $cashgame, $status = null){
			$id = $cashgame->getId();
			$location = $cashgame->getLocation();
			if($status == null){
				$status = $cashgame->getStatus();
			}
			$date = $cashgame->getStartTime();
			if($date == null){
				$date = $this->timer->getTime();
			}
			$dateStr = Globalization::formatIsoDate($date);
			return new RawCashgame($id, $location, $status, $dateStr);
		}
        */

	}

}