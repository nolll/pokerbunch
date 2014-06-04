using System.Globalization;
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
            var playerName = GetPlayerId(httpContext);
            return identity.IsAdmin || identity.Bunches.Any(userBunch => userBunch.Slug == slug && userBunch.Id.ToString(CultureInfo.InvariantCulture) == playerName);
        }

        private string GetPlayerId(HttpContextBase httpContext)
        {
            return httpContext.Request.RequestContext.RouteData.Values["playerId"] as string;
        }
    }
}