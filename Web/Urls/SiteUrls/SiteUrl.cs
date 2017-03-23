namespace Web.Urls.SiteUrls
{
    public abstract class SiteUrl : Url
    {
        protected SiteUrl(string url)
            : base(url)
        {
        }

        public override UrlType Type => UrlType.Site;
    }
}