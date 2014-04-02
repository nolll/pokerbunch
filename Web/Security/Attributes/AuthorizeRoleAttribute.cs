using System.Linq;
using System.Web;
using Core.Classes;

namespace Web.Security.Attributes
{
    public class AuthorizeRoleAttribute : AuthorizeCustomAttribute
    {
        protected bool AuthorizeCore(HttpContextBase httpContext, Role role)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                return false;
            }

            var identity = httpContext.User.Identity as CustomIdentity;
            if (identity == null)
            {
                return false;
            }

            if (role == Role.Admin && identity.IsAdmin)
            {
                return true;
            }

            var slug = GetSlug(httpContext);
            return identity.Bunches.Any(userBunch => userBunch.Slug == slug && userBunch.Role >= role);
        }

        protected string GetSlug(HttpContextBase httpContext)
        {
            return httpContext.Request.RequestContext.RouteData.Values["slug"] as string;
        }
    }
}