using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class Cashgame : ICacheable
    {
	    public int Id { get; private set; }
        public int BunchId { get; private set; }
        public string Location { get; private set; }
        public GameStatus Status { get; private set; }
        public bool IsStarted { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public IList<CashgameResult> Results { get; private set; }
        public int PlayerCount { get; private set; }
        public int Diff { get; private set; }
        public int Turnover { get; private set; }
        public bool HasActivePlayers { get; private set; }
        public int TotalStacks { get; private set; }
        public int AverageBuyin { get; private set; }
        public string DateString { get; private set; }

        public Cashgame(
                int id,
                int bunchId,
	            string location,
	            GameStatus status,
	            bool isStarted,
	            DateTime? startTime,
	            DateTime? endTime,
	            IList<CashgameResult> results,
	            int playerCount,
	            int diff,
	            int turnover,
	            bool hasActivePlayers,
                int totalStacks,
	            int averageBuyin,
                string dateString
            )
        {
            Id = id;
            BunchId = bunchId;
            Location = location;
            Status = status;
            IsStarted = isStarted;
            StartTime = startTime;
            EndTime = endTime;
            Results = results;
            PlayerCount = playerCount;
            Diff = diff;
            Turnover = turnover;
            HasActivePlayers = hasActivePlayers;
            TotalStacks = totalStacks;
            AverageBuyin = averageBuyin;
            DateString = dateString;
        }

        public int Duration
        {
            get
            {
                if (!StartTime.HasValue || !EndTime.HasValue)
                {
                    return 0;
                }
                var timespan = EndTime - StartTime;
                return (int) Math.Round(timespan.Value.TotalMinutes);
            }
        }

        public CashgameResult GetResult(int playerId)
	    {
	        return Results.FirstOrDefault(result => result.PlayerId == playerId);
	    }

        public bool IsInGame(int playerId)
        {
            return GetResult(playerId) != null;
        }

        public bool IsBestResult(CashgameResult resultToCheck)
        {
            var bestResult = GetBestResult();
            return bestResult != null && resultToCheck.Winnings == bestResult.Winnings;
        }

        public CashgameResult GetBestResult()
        {
            CashgameResult bestResult = null;
            foreach(var result in Results)
            {
                if(bestResult == null || result.Winnings > bestResult.Winnings){
                    bestResult = result;
                }
            }
            return bestResult;
        }
	}
}