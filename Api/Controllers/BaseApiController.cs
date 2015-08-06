using System.Web.Http;
using Web.Common;

namespace Api.Controllers
{
    public abstract class BaseApiController : ApiController
    {
        protected readonly UseCaseContainer UseCase = new UseCaseContainer();
    }
}