namespace Core.Urls
{
    public abstract class PlayerUrl : Url
    {
        protected PlayerUrl(string format, string slug, int playerId)
            : base(string.Format(format, slug, playerId))
        {
        }
    }
}