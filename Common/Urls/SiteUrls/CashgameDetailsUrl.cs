namespace PokerBunch.Common.Urls.SiteUrls
{
    public class CashgameDetailsUrl : SiteUrl
    {
        private readonly string _bunchId;
        private readonly string _cashgameId;

        public CashgameDetailsUrl(string bunchId, string cashgameId)
        {
            _bunchId = bunchId;
            _cashgameId = cashgameId;
        }

        protected override string Input => RouteParams.Replace(Route, RouteReplace.BunchId(_bunchId), RouteReplace.CashgameId(_cashgameId));
        public const string Route = "cashgame/details/{bunchId}/{cashgameId}";
    }
}