namespace Web.Urls
{
    public abstract class ApiUrl : Url
    {
        protected override string Host => SiteSettings.ApiHost;
    }
}