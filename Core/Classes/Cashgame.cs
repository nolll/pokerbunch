using System;

namespace Core.Classes{

	public class Cashgame{

	    public int Id { get; set; }
	    public string Location { get; set; }
	    public GameStatus Status { get; set; }
	    public bool IsStarted { get; set; }
	    public DateTime StartTime { get; set; }
	    public DateTime EndTime { get; set; }
	    public int Duration { get; set; }
        //public List<CashgameResult> Results { get; set; }
	    public int PlayerCount { get; set; }
	    public int Diff { get; set; }
	    public int Turnover { get; set; }
	    public bool HasActivePlayers { get; set; }
	    public int TotalStacks { get; set; }
	    public int AverageBuyin { get; set; }

	    public Cashgame()
	    {
            //Results = new List<CashgameResult>();
	        Status = GameStatus.Created;
            StartTime = new DateTime();
	    }

		/*
		public CashgameResult getResult(Player player){
			foreach(var result in Results){
				if(result.Player.Id() == player.Id()){
					return result;
				}
			}
			return null;
		}
        
		public function isInGame(Player $player){
			return $this->getResult($player) != null;
		}

		public function isBestResult(CashgameResult $resultToCheck){
			$bestResult = $this->getBestResult();
			return $bestResult != null && $resultToCheck->getWinnings() == $bestResult->getWinnings();
		}

		private function getBestResult(){
			$bestResult = null;
			foreach($this->results as $result){
				if($bestResult == null || $result->getWinnings() > $bestResult->getWinnings()){
					$bestResult = $result;
				}
			}
			return $bestResult;
		}

		public function getPlayerNames(){
			$playerNames = array();
			foreach($this->results as $result){
				$playerNames[] = $result->getPlayer()->getDisplayName();
			}
			return $playerNames;
		}
        */

	}

}