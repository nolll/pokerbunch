using System.Web;
using Core.Classes;

namespace Web.Security.Attributes
{
    public class AuthorizePlayerAttribute : AuthorizeRoleAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthorizeCore(httpContext, Role.Player);
        }
    }
}