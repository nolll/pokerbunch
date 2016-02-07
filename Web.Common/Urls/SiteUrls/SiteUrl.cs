namespace Web.Common.Urls.SiteUrls
{
    public abstract class SiteUrl : Url
    {
        protected SiteUrl(string url)
            : base(url)
        {
        }

        protected override string GetDomainName()
        {
            return CommonSettings.Instance.SiteHost;
        }
    }
}