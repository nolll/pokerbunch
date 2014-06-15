namespace Application.Urls
{
    public class PlayerIndexUrl : HomegameUrl
    {
        public PlayerIndexUrl(string slug)
            : base(RouteFormats.PlayerIndex, slug)
        {
        }
    }
}