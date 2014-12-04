using System.Web;

namespace Web.Security.Attributes
{
    public class AuthorizeAdminAttribute : AuthorizeRoleAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            var authorized = base.AuthorizeCore(httpContext);
            if (!authorized)
                return false;
            return Authorize.Admin(httpContext.User);
        }
    }
}