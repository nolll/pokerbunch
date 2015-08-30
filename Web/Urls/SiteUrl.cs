using Web.Common.Urls;

namespace Web.Urls
{
    public abstract class SiteUrl : Url
    {
        protected SiteUrl(string url)
            : base(url)
        {
        }

        public override string GetDomainName()
        {
            return "pokerbunch.com";
        }
    }
}