namespace Core.Urls
{
    public abstract class PlayerUrl : Url
    {
        protected PlayerUrl(string format, string slug, int playerId)
            : base(BuildUrl(format, slug, playerId))
        {
        }

        protected PlayerUrl(string format, int playerId)
            : this(format, "-", playerId)
        {
        }

        private static string BuildUrl(string format, string slug, int playerId)
        {
            var url = RouteParams.ReplaceSlug(format, slug);
            return RouteParams.ReplacePlayerId(url, playerId);
        }
    }
}