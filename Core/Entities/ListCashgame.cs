using System;
using System.Collections.Generic;
using System.Linq;

namespace Core.Entities
{
    public class ListCashgame
    {
        public string Id { get; }
        public DateTime StartTime { get; }
        public DateTime UpdatedTime { get; }
        public bool IsRunning { get; }
        public SmallLocation Location { get; }
        public IList<CashgamePlayer> Players { get; }

        public ListCashgame(string id, DateTime startTime, DateTime updatedTime, bool isRunning, SmallLocation location, IList<CashgamePlayer> players)
        {
            Id = id;
            StartTime = startTime;
            UpdatedTime = updatedTime;
            IsRunning = isRunning;
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
            public int PlayedMinutes => (int)Math.Round(PlayedTime.TotalMinutes);
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

        public int PlayerCount => Players.Count;
        public int Turnover => Players.Sum(o => o.Buyin);

        public int AverageBuyin
        {
            get
            {
                if (PlayerCount == 0)
                    return 0;
                return (int) Math.Round(Turnover/(double) PlayerCount);
            }
        }

        public CashgamePlayer GetResult(string playerId)
        {
            return Players.FirstOrDefault(o => o.Id == playerId);
        }

        public bool IsBestResult(CashgamePlayer resultToCheck)
        {
            var bestResult = GetBestResult();
            return bestResult != null && resultToCheck.Winnings == bestResult.Winnings;
        }

        public CashgamePlayer GetBestResult()
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
}