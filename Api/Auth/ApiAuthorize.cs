using System.Web.Http;
using System.Web.Http.Controllers;
using Web.Common;

namespace Api.Auth
{
    public class ApiAuthorize : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (IsTestEnvironment(actionContext))
                return true;
            return base.IsAuthorized(actionContext);
        }

        private bool IsTestEnvironment(HttpActionContext actionContext)
        {
            var hostName = GetHostName(actionContext);
            return Environment.IsDev(hostName);
        }

        private string GetHostName(HttpActionContext actionContext)
        {
            return actionContext.Request.RequestUri.Host;
        }
    }
}