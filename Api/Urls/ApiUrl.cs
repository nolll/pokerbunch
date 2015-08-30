using Web.Common.Urls;
using Web.Urls;

namespace Api.Urls
{
    public abstract class ApiUrl : Url
    {
        protected ApiUrl(string url)
            : base(url)
        {
        }

        public override string GetDomainName()
        {
            return "api.pokerbunch.com";
        }
    }

    public class TokenUrl : ApiUrl
    {
        public TokenUrl()
            : base(Routes.Token)
        {
        }
    }
}