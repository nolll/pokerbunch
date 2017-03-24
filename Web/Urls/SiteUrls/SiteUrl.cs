namespace Web.Urls.SiteUrls
{
    public abstract class SiteUrl : Url
    {
        public override UrlType Type => UrlType.Site;
    }
}