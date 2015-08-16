using System.Web.Http;
using Api.Models;
using Core.UseCases;

namespace Api.Controllers
{
    public class BunchController : BaseApiController
    {
        [Route("bunch")]
        [AcceptVerbs("GET")]
        public ApiBunchList List()
        {
            var bunchListResult = UseCase.BunchList.Execute(new BunchList.AllBunchesRequest("henriks"));
            return new ApiBunchList(bunchListResult);
        }

        [Route("bunch/{slug}")]
        [AcceptVerbs("GET")]
        public IHttpActionResult Details(string slug)
        {
            var bunchDetailsResult = UseCase.BunchDetails.Execute(new BunchDetails.Request(slug, "henriks"));
            var bunchModel = new ApiBunch(bunchDetailsResult.Slug, bunchDetailsResult.BunchName);
            return Ok(bunchModel);
        }
    }
}
