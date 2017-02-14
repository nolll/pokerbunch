using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Checkpoints;
using Core.Services;

namespace Core.Entities
{
    public class CashgameCollection
    {
        public CashgameBunch Bunch { get; }
        public IList<ListCashgame> Cashgames { get; }

        public CashgameCollection(CashgameBunch bunch, IList<ListCashgame> cashgames)
        {
            Bunch = bunch;
            Cashgames = cashgames;
        }
    }

    public class ListCashgame
    {
        public string Id { get; }
        public DateTime StartTime { get; }
        public DateTime UpdatedTime { get; }
        public bool IsRunning { get; }
        public Role Role { get; }
        public CashgameLocation Location { get; }
        public IList<CashgamePlayer> Players { get; }

        public ListCashgame(string id, DateTime startTime, DateTime updatedTime, bool isRunning, Role role, CashgameLocation location, IList<CashgamePlayer> players)
        {
            Id = id;
            StartTime = startTime;
            UpdatedTime = updatedTime;
            IsRunning = isRunning;
            Role = role;
            Location = location;
            Players = players;
        }

        public class CashgameBunch
        {
            public string Id { get; }
            public TimeZoneInfo Timezone { get; }
            public Currency Currency { get; }

            public CashgameBunch(string id, TimeZoneInfo timezone, Currency currency)
            {
                Id = id;
                Timezone = timezone;
                Currency = currency;
            }
        }

        public class CashgameLocation
        {
            public string Id { get; }
            public string Name { get; }

            public CashgameLocation(string id, string name)
            {
                Id = id;
                Name = name;
            }
        }

        public class CashgamePlayer
        {
            public string Id { get; }
            public string Name { get; }
            public string Color { get; }
            public int Stack { get; }
            public int Buyin { get; }
            public DateTime StartTime { get; }
            public DateTime UpdatedTime { get; }

            public CashgamePlayer(string id, string name, string color, int stack, int buyin, DateTime startTime, DateTime updatedTime)
            {
                Id = id;
                Name = name;
                Color = color;
                Stack = stack;
                Buyin = buyin;
                StartTime = startTime;
                UpdatedTime = updatedTime;
            }

            public int Winnings => Stack - Buyin;
            public int Winrate => PlayedMinutes == 0 ? 0 : (int)Math.Round((double)Winnings / PlayedMinutes * 60);
            private int PlayedMinutes => (int)Math.Round(PlayedTime.TotalMinutes);
            private TimeSpan PlayedTime => UpdatedTime - StartTime;
        }

        public int Duration
        {
            get
            {
                var timespan = UpdatedTime - StartTime;
                return (int)Math.Round(timespan.TotalMinutes);
            }
        }

        public int Turnover
        {
            get { return Players.Sum(o => o.Buyin); }
        }

        public bool IsBestResult(CashgamePlayer resultToCheck)
        {
            var bestResult = GetBestResult();
            return bestResult != null && resultToCheck.Winnings == bestResult.Winnings;
        }

        private CashgamePlayer GetBestResult()
        {
            CashgamePlayer bestResult = null;
            foreach (var result in Players)
            {
                if (bestResult == null || result.Winnings > bestResult.Winnings)
                {
                    bestResult = result;
                }
            }
            return bestResult;
        }
    }

    public class DetailedCashgame
    {
        public string Id { get; }
        public DateTime StartTime { get; }
        public DateTime UpdatedTime { get; }
        public bool IsRunning { get; }
        public CashgameBunch Bunch { get; }
        public Role Role { get; }
        public CashgameLocation Location { get; }
        public IList<CashgamePlayer> Players { get; }

        public DetailedCashgame(string id, DateTime startTime, DateTime updatedTime, bool isRunning, CashgameBunch bunch, Role role, CashgameLocation location, IList<CashgamePlayer> players)
        {
            Id = id;
            StartTime = startTime;
            UpdatedTime = updatedTime;
            IsRunning = isRunning;
            Bunch = bunch;
            Role = role;
            Location = location;
            Players = players;
        }

        public class CashgamePlayer
        {
            public string Id { get; }
            public string Name { get; }
            public string Color { get; }
            public int Stack { get; }
            public int Buyin { get; }
            public DateTime StartTime { get; }
            public DateTime UpdatedTime { get; }
            public IList<CashgameAction> Actions { get; set; }

            public CashgamePlayer(string id, string name, string color, int stack, int buyin, DateTime startTime, DateTime updatedTime, IList<CashgameAction> actions)
            {
                Id = id;
                Name = name;
                Color = color;
                Stack = stack;
                Buyin = buyin;
                StartTime = startTime;
                UpdatedTime = updatedTime;
                Actions = actions;
            }

            public int Winnings => Stack - Buyin;
            public int Winrate => PlayedMinutes == 0 ? 0 : (int)Math.Round((double)Winnings / PlayedMinutes * 60);
            private int PlayedMinutes => (int)Math.Round(PlayedTime.TotalMinutes);
            private TimeSpan PlayedTime => UpdatedTime - StartTime;
        }

        public class CashgameAction
        {
            public string Id { get; }
            public CheckpointType Type { get; }
            public DateTime Time { get; }
            public int Stack { get; }
            public int Added { get; }

            public CashgameAction(string id, CheckpointType type, DateTime time, int stack, int added)
            {
                Id = id;
                Type = type;
                Time = time;
                Stack = stack;
                Added = added;
            }
        }
    }

    public class Cashgame : IEntity
    {
        public IList<Checkpoint> Checkpoints { get; private set; }
        public IList<Checkpoint> AddedCheckpoints { get; }
        public IList<Checkpoint> UpdatedCheckpoints { get; }
        public IList<Checkpoint> DeletedCheckpoints { get; }
        public string Id { get; }
        public string BunchId { get; }
        public string LocationId { get; private set; }
        public GameStatus Status { get; private set; }
        public DateTime? StartTime { get; private set; }
        public DateTime? EndTime { get; private set; }
        public IList<CashgameResult> Results { get; private set; }
        public int PlayerCount { get; private set; }
        public int Turnover { get; private set; }
        public int AverageBuyin { get; private set; }
        public string DateString { get; private set; }
        public string CacheId => Id;

        public Cashgame(string bunchId, string locationId, GameStatus status, string id = null, IList<Checkpoint> checkpoints = null)
        {
            Id = id ?? "";
            BunchId = bunchId;
            LocationId = locationId;
            Status = status;
            AddCheckpoints(checkpoints);
            AddedCheckpoints = new List<Checkpoint>();
            UpdatedCheckpoints = new List<Checkpoint>();
            DeletedCheckpoints = new List<Checkpoint>();
        }

        public void ChangeStatus(GameStatus status)
        {
            Status = status;
        }

        public void AddCheckpoints(IList<Checkpoint> checkpoints)
        {
            Checkpoints = checkpoints ?? new List<Checkpoint>();
            Results = CreateResults(Checkpoints);
            StartTime = GetStartTime(Results);
            EndTime = GetEndTime(Results);
            PlayerCount = Results.Count;
            Turnover = GetBuyinSum(Results);
            AverageBuyin = GetAverageBuyin(Turnover, PlayerCount);
            DateString = StartTime.HasValue ? Globalization.FormatIsoDate(StartTime.Value) : string.Empty;
        }

        public Checkpoint GetCheckpoint(string checkpointId)
        {
            return Checkpoints.FirstOrDefault(o => o.Id == checkpointId);
        }

        private static IList<CashgameResult> CreateResults(IEnumerable<Checkpoint> checkpoints)
        {
            var map = new Dictionary<string, IList<Checkpoint>>();
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

        private static int GetAverageBuyin(int turnover, int playerCount)
        {
            if (playerCount == 0)
                return 0;
            return (int)Math.Round(turnover / (double)playerCount);
        }

        public void AddCheckpoint(Checkpoint checkpoint)
        {
            Checkpoints.Add(checkpoint);
            AddedCheckpoints.Add(checkpoint);
        }

        public void UpdateCheckpoint(Checkpoint checkpoint)
        {
            var oldCheckpoint = Checkpoints.First(o => o.Id == checkpoint.Id);
            Checkpoints[Checkpoints.IndexOf(oldCheckpoint)] = checkpoint;
            UpdatedCheckpoints.Add(checkpoint);
        }

        public void DeleteCheckpoint(Checkpoint checkpoint)
        {
            Checkpoints.Remove(checkpoint);
            DeletedCheckpoints.Add(checkpoint);
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

        public CashgameResult GetResult(string playerId)
	    {
	        return Results.FirstOrDefault(result => result.PlayerId == playerId);
	    }

        public bool IsInGame(string playerId)
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

    public class CashgameBunch
    {
        public string Id { get; }
        public TimeZoneInfo Timezone { get; }
        public Currency Currency { get; }

        public CashgameBunch(string id, TimeZoneInfo timezone, Currency currency)
        {
            Id = id;
            Timezone = timezone;
            Currency = currency;
        }
    }

    public class CashgameLocation
    {
        public string Id { get; }
        public string Name { get; }

        public CashgameLocation(string id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}