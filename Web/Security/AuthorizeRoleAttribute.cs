using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Exceptions;
using Core.Classes;

namespace Web.Security
{
    public class AuthorizeRoleAttribute : AuthorizeAttribute
    {
        public Role Role { get; set; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
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

            if (Role == Role.Admin && identity.IsAdmin)
            {
                return true;
            }

            var slug = httpContext.Request.RequestContext.RouteData.Values["slug"] as string;
            return identity.Bunches.Any(userBunch => userBunch.Slug == slug && userBunch.Role >= Role);
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            base.OnAuthorization(filterContext);
            if (filterContext.HttpContext.User.Identity.IsAuthenticated && filterContext.Result is HttpUnauthorizedResult)
            {
                throw new AccessDeniedException();
            }
        }
    }
}