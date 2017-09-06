using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class AddCashgameUrl : SiteUrl
    {
        private readonly string _slug;

        public AddCashgameUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.Add, RouteReplace.Slug(_slug));
    }
}