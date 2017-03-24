using Web.Routes;

namespace Web.Urls.SiteUrls
{
    public class EditUserUrl : SiteUrl
    {
        private readonly string _userName;

        public EditUserUrl(string userName)
        {
            _userName = userName;
        }

        protected override string Input => RouteParams.Replace(WebRoutes.User.Edit, RouteParam.UserName(_userName));
    }
}