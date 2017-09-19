namespace Web.Urls.SiteUrls
{
    public class AddUserUrl : SiteUrl
    {
        public const string Route = "user/add";
        protected override string Input => Route;
    }
}