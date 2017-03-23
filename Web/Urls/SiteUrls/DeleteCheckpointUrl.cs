using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class DeleteCheckpointUrl : SiteUrl
    {
        public DeleteCheckpointUrl(string cashgameId, string id)
            : base(BuildUrl(WebRoutes.Cashgame.CheckpointDelete, cashgameId, id))
        {
        }

        private static string BuildUrl(string format, string cashgameId, string id)
        {
            var url = RouteParams.ReplaceCashgameId(format, cashgameId);
            return RouteParams.ReplaceId(url, id);
        }
    }
}