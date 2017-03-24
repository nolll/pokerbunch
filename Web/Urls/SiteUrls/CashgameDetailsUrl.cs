using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class CashgameDetailsUrl : SiteUrl
    {
        private readonly string _id;

        public CashgameDetailsUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Cashgame.Details, RouteParam.Id(_id));
    }
}