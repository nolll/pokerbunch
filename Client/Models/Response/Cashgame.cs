using System;
using System.Collections.Generic;

namespace PokerBunch.Client.Models.Response
{
    public class Cashgame
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool IsRunning { get; set; }
        public CashgameBunch Bunch { get; set; }
        public string Role { get; set; }
        public CashgameLocation Location { get; set; }
        public CashgameEvent Event { get; set; }
        public IList<CashgamePlayer> Players { get; set; }
    }
}