namespace Core.Urls
{
    public abstract class CheckpointUrl : Url
    {
        protected CheckpointUrl(string format, string slug, string dateStr, int playerId, int checkpointId)
            : base(BuildUrl(format, slug, dateStr, playerId, checkpointId))
        {
        }

        private static string BuildUrl(string format, string slug, string dateStr, int playerId, int checkpointId)
        {
            var url = RouteParams.ReplaceSlug(format, slug);
            url = RouteParams.ReplaceDateStr(url, dateStr);
            url = RouteParams.ReplacePlayerId(url, playerId);
            url = RouteParams.ReplaceCheckpointId(url, checkpointId);
            return url;
        }
    }
}