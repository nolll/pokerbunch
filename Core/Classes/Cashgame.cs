using System;
using System.Collections.Generic;

namespace Core.Classes{
    public class Cashgame{

	    public int Id { get; private set; }
        public string Location { get; private set; }
        public GameStatus Status { get; private set; }
        public bool IsStarted { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public int Duration { get; private set; }
        public IList<CashgameResult> Results { get; protected set; }
        public int PlayerCount { get; private set; }
        public int Diff { get; private set; }
        public int Turnover { get; private set; }
        public bool HasActivePlayers { get; private set; }
        public int TotalStacks { get; private set; }
        public int AverageBuyin { get; private set; }

        public Cashgame(
                int id,
	            string location,
	            GameStatus status,
	            bool isStarted,
	            DateTime? startTime,
	            DateTime? endTime,
	            int duration,
                IList<CashgameResult> results,
	            int playerCount,
	            int diff,
	            int turnover,
	            bool hasActivePlayers,
                int totalStacks,
	            int averageBuyin
            )
        {
            Id = id;
            Location = location;
            Status = status;
            IsStarted = isStarted;
            StartTime = startTime;
            EndTime = endTime;
            Duration = duration;
            Results = results;
            PlayerCount = playerCount;
            Diff = diff;
            Turnover = turnover;
            HasActivePlayers = hasActivePlayers;
            TotalStacks = totalStacks;
            AverageBuyin = averageBuyin;
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