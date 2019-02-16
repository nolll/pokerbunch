using System;
using System.Collections.Generic;

namespace PokerBunch.Client.Models.Response
{
    public class CashgameSmall
    {
        public string Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public bool IsRunning { get; set; }
        public string Role { get; set; }
        public CashgameLocation Location { get; set; }
        public IList<CashgameSmallPlayer> Players { get; set; }
    }
}