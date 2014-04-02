using System.Linq;
using System.Web;

namespace Web.Security.Attributes
{
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
}