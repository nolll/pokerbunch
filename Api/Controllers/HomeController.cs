using System.Web.Http;
using Api.Urls;

namespace Api.Controllers
{
    public class HomeController : BaseApiController
    {
        [Route(Routes.Home)]
        [AcceptVerbs("GET")]
        public IHttpActionResult Home()
        {
            return Redirect("https://pokerbunch.com/api");
        }
    }
}