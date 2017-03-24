using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class DeletePlayerUrl : SiteUrl
    {
        private readonly string _id;

        public DeletePlayerUrl(string id)
        {
            _id = id;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Player.Delete, RouteParam.Id(_id));
    }
}