namespace Web.Urls
{
    public enum UrlType
    {
        Site,
        Api
    }

    public abstract class Url
    {
        public abstract UrlType Type { get; }
        protected abstract string Input { get; }
        public string Relative => Input != null ? string.Concat("/", Input) : string.Empty;
        public override string ToString() => Relative;
    }

    public static class AbsoluteUrl
    {
        public static string Create(Url url, string siteHost, string apiHost)
        {
            var domainName = GetDomainName(url.Type, siteHost, apiHost);
            var relativeUrl = url.Relative;
            return $"https://{domainName}{relativeUrl}";
        }
        
        private static string GetDomainName(UrlType type, string siteHost, string apiHost)
        {
            return type == UrlType.Api ? apiHost : siteHost;
        }
    }
}