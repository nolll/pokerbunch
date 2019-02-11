using System;
using System.Collections.Generic;
using System.Linq;
using Core.Entities.Checkpoints;

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

        public CashgameAction GetAction(string id)
        {
            return Players.SelectMany(p => p.Actions).FirstOrDefault(a => a.Id == id);
        }

        public class CashgamePlayer
        {
            public string Id { get; }
            public string Name { get; }
            public string Color { get; }
            public int Stack { get; }
            public int Buyin { get; }
            private DateTime StartTime { get; }
            private DateTime UpdatedTime { get; }
            public IList<CashgameAction> Actions { get; }

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
            public string PlayerId { get; }
            public CheckpointType Type { get; }
            public DateTime Time { get; }
            public int Stack { get; }
            public int Added { get; }

            public CashgameAction(string id, string playerId, CheckpointType type, DateTime time, int stack, int added)
            {
                Id = id;
                PlayerId = playerId;
                Type = type;
                Time = time;
                Stack = stack;
                Added = added;
            }
        }
    }
}