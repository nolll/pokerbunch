namespace Web.Urls
{
    public class PlayerIndexUrl : SlugUrl
    {
        public PlayerIndexUrl(string slug)
            : base(Routes.PlayerList, slug)
        {
        }
    }
}