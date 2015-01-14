using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Checkpoints;
using Core.Services;

namespace Core.Entities
{
    public class Cashgame : IEntity
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
            Results = results ?? new List<CashgameResult>();
            PlayerCount = playerCount;
            Diff = diff;
            Turnover = turnover;
            HasActivePlayers = hasActivePlayers;
            TotalStacks = totalStacks;
            AverageBuyin = averageBuyin;
            DateString = dateString;
        }

        public Cashgame(int bunchId, string location, GameStatus status, int? id = null, IEnumerable<Checkpoint> checkpoints = null)
        {
            Results = checkpoints != null ? CreateResults(checkpoints) : new List<CashgameResult>();
            Id = id ?? 0;
            BunchId = bunchId;
            Location = location;
            Status = status;
            StartTime = GetStartTime(Results);
            IsStarted = StartTime.HasValue;
            EndTime = GetEndTime(Results);
            PlayerCount = Results.Count;
            Turnover = GetBuyinSum(Results);
            Diff = Turnover - GetCashoutSum(Results);
            HasActivePlayers = Results.Any(result => !result.CashoutTime.HasValue);
            TotalStacks = Results.Sum(result => result.Stack);
            AverageBuyin = GetAverageBuyin(Turnover, PlayerCount);
            var startTime = GetStartTime(Results);
            var dateString = startTime.HasValue ? Globalization.FormatIsoDate(StartTime.Value) : string.Empty;
            DateString = dateString;
        }

        private static IList<CashgameResult> CreateResults(IEnumerable<Checkpoint> checkpoints)
        {
            var map = new Dictionary<int, IList<Checkpoint>>();
            foreach (var checkpoint in checkpoints)
            {
                IList<Checkpoint> list;
                if (!map.TryGetValue(checkpoint.PlayerId, out list))
                {
                    list = new List<Checkpoint>();
                    map.Add(checkpoint.PlayerId, list);
                }
                list.Add(checkpoint);
            }

            var results = new List<CashgameResult>();
            foreach (var playerKey in map.Keys)
            {
                var playerCheckpoints = map[playerKey].OrderBy(o => o.Timestamp).ToList();
                var playerResults = new CashgameResult(playerKey, playerCheckpoints);
                results.Add(playerResults);
            }
            return results;
        }

        private static DateTime? GetStartTime(IEnumerable<CashgameResult> results)
        {
            DateTime? startTime = null;
            foreach (var result in results)
            {
                if (!startTime.HasValue || result.BuyinTime < startTime)
                {
                    startTime = result.BuyinTime;
                }
            }
            return startTime;
        }

        private static DateTime? GetEndTime(IEnumerable<CashgameResult> results)
        {
            DateTime? endTime = null;
            foreach (var result in results)
            {
                if (!endTime.HasValue || result.CashoutTime > endTime)
                {
                    endTime = result.CashoutTime;
                }
            }
            return endTime;
        }

        private static int GetBuyinSum(IEnumerable<CashgameResult> results)
        {
            return results.Sum(result => result.Buyin);
        }

        private static int GetCashoutSum(IEnumerable<CashgameResult> results)
        {
            return results.Sum(result => result.Stack);
        }

        private static int GetAverageBuyin(int turnover, int playerCount)
        {
            if (playerCount == 0)
                return 0;
            return (int)Math.Round(turnover / (double)playerCount);
        }
        
        public int Duration
        {
            get
            {
                if (!StartTime.HasValue || !EndTime.HasValue)
                    return 0;
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