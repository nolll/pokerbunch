namespace Web.Urls.SiteUrls
{
    public abstract class SiteUrl : Url
    {
        protected override string Host => SiteSettings.SiteHost;
    }
}