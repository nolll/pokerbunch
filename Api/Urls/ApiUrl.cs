using Web.Common.Urls;

namespace Api.Urls
{
    public abstract class ApiUrl : Url
    {
        protected ApiUrl(string url)
            : base(url)
        {
        }

        protected override string GetDomainName()
        {
            return "api.pokerbunch.com";
        }
    }
}