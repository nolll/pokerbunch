using System.Linq;
using System.Web;
using System.Web.Mvc;
using Application.Exceptions;
using Core.Classes;

namespace Web.Security
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

    public class AuthorizeAdminAttribute : AuthorizeRoleAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthorizeCore(httpContext, Role.Admin);
        }
    }

    public class AuthorizeManagerAttribute : AuthorizeRoleAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthorizeCore(httpContext, Role.Manager);
        }
    }

    public class AuthorizePlayerAttribute : AuthorizeRoleAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthorizeCore(httpContext, Role.Player);
        }
    }

    public class AuthorizeOwnPlayerAttribute : AuthorizePlayerAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
            {
                return false;
            }

            var identity = GetIdentity(httpContext);
            var slug = GetSlug(httpContext);
            var playerName = GetPlayerName(httpContext);
            return identity.IsAdmin || identity.Bunches.Any(userBunch => userBunch.Slug == slug && userBunch.Name.ToLower() == playerName);
        }

        private string GetPlayerName(HttpContextBase httpContext)
        {
            return httpContext.Request.RequestContext.RouteData.Values["playerName"] as string;
        }
    }

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