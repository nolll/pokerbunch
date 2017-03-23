using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditCheckpointUrl : SiteUrl
    {
        public EditCheckpointUrl(string cashgameId, string id)
            : base(BuildUrl(WebRoutes.Cashgame.CheckpointEdit, cashgameId, id))
        {
        }

        private static string BuildUrl(string format, string cashgameId, string id)
        {
            var url = RouteParams.ReplaceCashgameId(format, cashgameId);
            return RouteParams.ReplaceId(url, id);
        }
    }
}