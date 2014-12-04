using System.Web;
using Core.Entities;

namespace Web.Security.Attributes
{
    public class AuthorizeRoleAttribute : AuthorizeCustomAttribute
    {
        protected bool AuthorizeCore(HttpContextBase httpContext, Role role)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
                return false;
            return Authorize.Bunch(httpContext.User, GetSlug(httpContext), role);
        }

        protected string GetSlug(HttpContextBase httpContext)
        {
            return httpContext.Request.RequestContext.RouteData.Values["slug"] as string;
        }
    }
}