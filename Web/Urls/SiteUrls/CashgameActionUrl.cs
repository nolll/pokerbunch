using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameActionUrl : SiteUrl
    {
        public CashgameActionUrl(string cashgameId, string playerId)
            : base(BuildUrl(WebRoutes.Cashgame.Action, cashgameId, playerId))
        {
        }

        private static string BuildUrl(string format, string cashgameId, string playerId)
        {
            var url = RouteParams.ReplaceCashgameId(format, cashgameId);
            return RouteParams.ReplacePlayerId(url, playerId);
        }
    }
}