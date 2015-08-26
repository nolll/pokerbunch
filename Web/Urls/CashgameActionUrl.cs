namespace Web.Urls
{
    public class CashgameActionUrl : Url
    {
        public CashgameActionUrl(int cashgameId, int playerId)
            : base(BuildUrl(Routes.CashgameAction, cashgameId, playerId))
        {
        }

        private static string BuildUrl(string format, int cashgameId, int playerId)
        {
            var url = RouteParams.ReplaceCashgameId(format, cashgameId);
            return RouteParams.ReplacePlayerId(url, playerId);
        }
    }
}