using System.Web.Http;
using Web.Common;

namespace Api.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected readonly UseCaseContainer UseCase = new UseCaseContainer();

        protected string CurrentUserName
        {
            get
            {
                if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                    return User.Identity.Name;
                return null;
            }
        }
    }
}