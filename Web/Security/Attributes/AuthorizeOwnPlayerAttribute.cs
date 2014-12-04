using System.Web;

namespace Web.Security.Attributes
{
    public class AuthorizeOwnPlayerAttribute : AuthorizePlayerAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
                return false;
            var slug = GetSlug(httpContext);
            var playerId = GetPlayerId(httpContext);
            return Authorize.SpecificPlayer(httpContext.User, slug, playerId);
        }

        private int GetPlayerId(HttpContextBase httpContext)
        {
            var str = httpContext.Request.RequestContext.RouteData.Values["playerId"] as string;
            return str != null ? int.Parse(str) : 0;
        }
    }
}