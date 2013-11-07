using System;
using System.Collections.Generic;

namespace Core.Classes{

	public class Cashgame{

	    public int Id { get; set; }
	    public string Location { get; set; }
	    public GameStatus Status { get; set; }
	    public bool IsStarted { get; set; }
	    public DateTime? StartTime { get; set; }
	    public DateTime? EndTime { get; set; }
	    public int Duration { get; set; }
        public IList<CashgameResult> Results { get; set; }
	    public int PlayerCount { get; set; }
	    public int Diff { get; set; }
	    public int Turnover { get; set; }
	    public bool HasActivePlayers { get; set; }
	    public int TotalStacks { get; set; }
	    public int AverageBuyin { get; set; }

	    public Cashgame()
	    {
            Results = new List<CashgameResult>();
	        Status = GameStatus.Created;
	    }

		public CashgameResult GetResult(Player player){
			foreach(var result in Results){
				if(result.Player.Id == player.Id){
					return result;
				}
			}
			return null;
		}

        public bool IsInGame(Player player){
            return GetResult(player) != null;
        }

        public bool IsBestResult(CashgameResult resultToCheck){
            var bestResult = GetBestResult();
            return bestResult != null && resultToCheck.Winnings == bestResult.Winnings;
        }

        private CashgameResult GetBestResult(){
            CashgameResult bestResult = null;
            foreach(var result in Results){
                if(bestResult == null || result.Winnings > bestResult.Winnings){
                    bestResult = result;
                }
            }
            return bestResult;
        }

        public List<string> GetPlayerNames(){
            var playerNames = new List<string>();
            foreach(var result in Results){
                playerNames.Add(result.Player.DisplayName);
            }
            return playerNames;
        }

	}

}