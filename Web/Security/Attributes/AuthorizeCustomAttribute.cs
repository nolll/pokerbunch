using System.Web.Mvc;
using Core.Exceptions;

namespace Web.Security.Attributes
{
    public abstract class AuthorizeCustomAttribute : AuthorizeAttribute
    {
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