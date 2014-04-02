using System.Web;

namespace Web.Security.Attributes
{
    public class AuthorizeOwnUserAttribute : AuthorizeCustomAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                return false;
            }

            var identity = GetIdentity(httpContext);
            var userName = GetUserName(httpContext);
            return identity.IsAdmin || identity.UserName.ToLower() == userName;
        }

        private string GetUserName(HttpContextBase httpContext)
        {
            return httpContext.Request.RequestContext.RouteData.Values["userName"] as string;
        }
    }
}