using System.Web;
using Core.Classes;

namespace Web.Security.Attributes
{
    public class AuthorizeManagerAttribute : AuthorizeRoleAttribute
    {
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            return AuthorizeCore(httpContext, Role.Manager);
        }
    }
}