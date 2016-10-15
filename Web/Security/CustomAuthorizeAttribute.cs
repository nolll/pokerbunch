using System.Web;

namespace Web.Security
{
    public class CustomAuthorizeAttribute : System.Web.Mvc.AuthorizeAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var isAuthorized = base.AuthorizeCore(httpContext);
            if (!isAuthorized)
                return false;
            var identity = new Identity(httpContext.User);
            if (!identity.IsAuthenticated)
                return false;
            return true;
        }
    }
}