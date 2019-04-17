using System;
using System.Collections.Generic;

namespace Core.Entities
{
    public class DetailedCashgame
    {
        public string Id { get; }
        public DateTime StartTime { get; }
        public DateTime UpdatedTime { get; }
        public bool IsRunning { get; }
        public CashgameBunch Bunch { get; }
        public Role Role { get; }
        public CashgameLocation Location { get; }
        public CashgameEvent Event { get; }
        public IList<CashgamePlayer> Players { get; }

        public DetailedCashgame(string id, DateTime startTime, DateTime updatedTime, bool isRunning, CashgameBunch bunch, Role role, CashgameLocation location, CashgameEvent @event, IList<CashgamePlayer> players)
        {
            Id = id;
            StartTime = startTime;
            UpdatedTime = updatedTime;
            IsRunning = isRunning;
            Bunch = bunch;
            Role = role;
            Location = location;
            Event = @event;
            Players = players;
        }

        public bool BelongsToEvent => Event != null;

        public class CashgamePlayer
        {
            public string Id { get; }
            public string Name { get; }
            public string Color { get; }
            public int Stack { get; }
            public int Buyin { get; }
            private DateTime StartTime { get; }
            private DateTime UpdatedTime { get; }

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
        }
    }
}