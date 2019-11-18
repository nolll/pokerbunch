using System;
using PokerBunch.Common.Urls.ApiUrls;

namespace PokerBunch.Common.Urls
{
    public abstract class Url
    {
        protected abstract string Input { get; }
        public string Relative => Input != null ? string.Concat("/", Input).ToLower() : string.Empty;
        public string Absolute(string host, int port = 80, string protocol = "http")
        {
            var hostAndPort = port == 80 ? host : $"{host}:{port}";
            return $"{protocol}://{hostAndPort}{Relative}";
        }
    }

    public class UrlFormatter : IUrlFormatter
    {
        private readonly Uri _siteUri;
        private readonly Uri _apiUri;

        public UrlFormatter(Uri siteUri, Uri apiUri)
        {
            _siteUri = siteUri;
            _apiUri = apiUri;
        }

        public string ToAbsolute(ApiUrl url)
        {
            return new Uri(_apiUri, url.Relative).AbsoluteUri;
        }
    }

    public interface IUrlFormatter
    {
        string ToAbsolute(ApiUrl url);
    }
}
