using Web.Routing;

namespace Web.Models.UrlModels
{
    public class ForgotPasswordUrl : Url
    {
        public ForgotPasswordUrl()
            : base(RouteFormats.ForgotPassword)
        {
        }
    }
}