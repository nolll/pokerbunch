using System;
using System.Collections.Generic;

namespace PokerBunch.Client.Models.Response
{
    public class CashgamePlayer
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Stack { get; set; }
        public int Buyin { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime UpdatedTime { get; set; }
        public IList<CashgameAction> Actions { get; set; }
    }
}