using System.Web.Http;
using Web.Common;
using Web.Common.Cache;

namespace Api.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        private readonly Bootstrap _bootstrap = new Bootstrap();

        protected UseCaseContainer UseCase
        {
            get { return _bootstrap.UseCases; }
        }

        protected CacheBuster Buster
        {
            get { return _bootstrap.CacheBuster; }
        }

        protected string CurrentUserName
        {
            get
            {
                if (User == null || User.Identity == null)
                    return null;
                if (User.Identity.IsAuthenticated)
                    return User.Identity.Name;
                if (Environment.IsDev(Request.RequestUri.Host))
                    return "henriks";
                return null;
            }
        }
    }
}