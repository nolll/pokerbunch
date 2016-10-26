namespace Web.Urls.SiteUrls
{
    public class AddCashgameUrl : SlugUrl
    {
        public AddCashgameUrl(string slug)
            : base(WebRoutes.Cashgame.Add, slug)
        {
        }
    }
}