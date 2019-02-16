using JetBrains.Annotations;

namespace PokerBunch.Client.Models.Request
{
    public class EventCashgameAdd
    {
        [UsedImplicitly]
        public string CashgameId { get; set; }

        public EventCashgameAdd(string cashgameId)
        {
            CashgameId = cashgameId;
        }
    }
}