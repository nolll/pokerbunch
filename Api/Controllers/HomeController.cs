using System.Web.Http;
using Web.Common.Routes;

namespace Api.Controllers
{
    public class HomeController : BaseApiController
    {
        [Route(ApiRoutes.Home)]
        [AcceptVerbs("GET")]
        public IHttpActionResult Home()
        {
            return Redirect("https://pokerbunch.com/api");
        }
    }
}