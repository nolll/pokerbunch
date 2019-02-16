using System;

namespace PokerBunch.Client.Models.Response
{
    public class CashgameAction
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public DateTime Time { get; set; }
        public int Stack { get; set; }
        public int Added { get; set; }
    }
}