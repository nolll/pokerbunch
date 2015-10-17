using System.Web.Http;
using System.Web.Http.Controllers;
using Web.Common.Services;

namespace Api.Auth
{
    public class ApiAuthorizeAttribute : AuthorizeAttribute
    {
        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            if (RequestEvaluator.IsTestEnvironment(actionContext.Request))
                return true;
            return base.IsAuthorized(actionContext);
        }
    }
}