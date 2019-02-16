namespace PokerBunch.Client.Models.Request
{
    public class BunchJoin
    {
        public string BunchId { get; }
        public string Code { get; }

        public BunchJoin(string bunchId, string code)
        {
            BunchId = bunchId;
            Code = code;
        }
    }
}