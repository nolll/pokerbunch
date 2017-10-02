using System;
using System.Collections.Generic;
using Core.Entities;
using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    internal class ApiDetailedCashgame
    {
        [UsedImplicitly]
        public string Id { get; set; }
        [UsedImplicitly]
        public DateTime StartTime { get; set; }
        [UsedImplicitly]
        public DateTime UpdatedTime { get; set; }
        [UsedImplicitly]
        public bool IsRunning { get; set; }
        [UsedImplicitly]
        public ApiCashgameBunch Bunch { get; set; }
        [UsedImplicitly]
        public Role Role { get; set; }
        [UsedImplicitly]
        public ApiCashgameLocation Location { get; set; }
        [UsedImplicitly]
        public ApiCashgameEvent Event { get; set; }
        [UsedImplicitly]
        public IList<ApiDetailedCashgamePlayer> Players { get; set; }

        public class ApiDetailedCashgamePlayer
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Name { get; set; }
            [UsedImplicitly]
            public string Color { get; set; }
            [UsedImplicitly]
            public int Stack { get; set; }
            [UsedImplicitly]
            public int Buyin { get; set; }
            [UsedImplicitly]
            public DateTime StartTime { get; set; }
            [UsedImplicitly]
            public DateTime UpdatedTime { get; set; }
            [UsedImplicitly]
            public IList<ApiDetailedCashgameAction> Actions { get; set; }
        }

        public class ApiDetailedCashgameAction
        {
            [UsedImplicitly]
            public string Id { get; set; }
            [UsedImplicitly]
            public string Type { get; set; }
            [UsedImplicitly]
            public DateTime Time { get; set; }
            [UsedImplicitly]
            public int Stack { get; set; }
            [UsedImplicitly]
            public int Added { get; set; }
        }
    }
}