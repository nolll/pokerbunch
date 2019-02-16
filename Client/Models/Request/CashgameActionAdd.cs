namespace PokerBunch.Client.Models.Request
{
    public abstract class CashgameActionAdd
    {
        public string Type { get; }
        public string CashgameId { get; }
        public string PlayerId { get; }
        public int Added { get; }
        public int Stack { get; }

        protected CashgameActionAdd(string type, string cashgameId, string playerId, int added, int stack)
        {
            Type = type;
            CashgameId = cashgameId;
            PlayerId = playerId;
            Added = added;
            Stack = stack;
        }
    }
}