using Web.Common.Routes;

namespace Web.Urls
{
    public class ApiCashoutUrl : ApiUrl
    {
        public ApiCashoutUrl(string slug)
            : base(ApiRoutes.Cashout)
        {
        }
    }
}