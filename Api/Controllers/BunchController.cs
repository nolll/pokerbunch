using System.Web.Http;
using Api.Models;
using Core.UseCases;
using Web.Common.Routes;

namespace Api.Controllers
{
    public class BunchController : BaseApiController
    {
        [Route(ApiRoutes.BunchList)]
        [AcceptVerbs("GET")]
        public ApiBunchList List()
        {
            var bunchListResult = UseCase.BunchList.Execute(new BunchList.AllBunchesRequest(CurrentUserName));
            return new ApiBunchList(bunchListResult);
        }

        [Route(ApiRoutes.BunchDetails)]
        [AcceptVerbs("GET")]
        public IHttpActionResult Details(string slug)
        {
            var bunchDetailsResult = UseCase.BunchDetails.Execute(new BunchDetails.Request(CurrentUserName, slug));
            var bunchModel = new ApiBunch(bunchDetailsResult.Slug, bunchDetailsResult.BunchName);
            return Ok(bunchModel);
        }
    }
}
