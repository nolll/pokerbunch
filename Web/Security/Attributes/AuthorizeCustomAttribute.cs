using System.Web;
using System.Web.Mvc;
using Application;
using Application.Exceptions;

namespace Web.Security.Attributes
{
    public abstract class AuthorizeCustomAttribute : AuthorizeAttribute
    {
        protected CustomIdentity GetIdentity(HttpContextBase httpContext)
        {
            return httpContext.User.Identity as CustomIdentity;
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