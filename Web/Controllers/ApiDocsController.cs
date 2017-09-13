using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.AppModels.Details;
using Web.Routes;

namespace Web.Controllers
{
    public class ApiDocsController : BaseController
    {
        [Route(WebRoutes.Api.Docs)]
        public ActionResult ApiDocs()
        {
            var context = GetAppContext();
            var model = new ApiDocsPageModel(context);
            return View(model);
        }
    }
}