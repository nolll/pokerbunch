using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameActionUrl : SiteUrl
    {
        private readonly string _cashgameId;
        private readonly string _playerId;

        public CashgameActionUrl(string cashgameId, string playerId)
        {
            _cashgameId = cashgameId;
            _playerId = playerId;
        }
        
        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.Action, RouteReplace.CashgameId(_cashgameId), RouteReplace.PlayerId(_playerId));
    }
}