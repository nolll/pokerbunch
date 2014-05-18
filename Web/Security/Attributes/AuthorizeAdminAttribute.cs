using System.Web;
using Core.Entities;

namespace Web.Security.Attributes
{
    public class AuthorizeAdminAttribute : AuthorizeRoleAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthorizeCore(httpContext, Role.Admin);
        }
    }
}