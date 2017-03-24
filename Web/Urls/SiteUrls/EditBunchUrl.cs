using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditBunchUrl : SiteUrl
    {
        private readonly string _slug;

        public EditBunchUrl(string slug)
        {
            _slug = slug;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.Bunch.Edit, RouteParam.Slug(_slug));
    }
}