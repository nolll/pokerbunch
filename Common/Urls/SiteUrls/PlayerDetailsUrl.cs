namespace PokerBunch.Common.Urls.SiteUrls
{
    public class PlayerDetailsUrl : SiteUrl
    {
        private readonly string _bunchId;
        private readonly string _playerId;

        public PlayerDetailsUrl(string bunchId, string playerId)
        {
            _bunchId = bunchId;
            _playerId = playerId;
        }

        protected override string Input => RouteParams.Replace(Route, RouteReplace.BunchId(_bunchId), RouteReplace.PlayerId(_playerId));
        public const string Route = "bunches/{bunchId}/players/{playerId}";
    }
}