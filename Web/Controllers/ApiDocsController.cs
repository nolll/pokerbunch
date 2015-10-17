using System.Web.Mvc;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.AppModels.Details;

namespace Web.Controllers
{
    public class ApiDocsController : BaseController
    {
        [Route(WebRoutes.Api.Docs)]
        public ActionResult ApiDocs()
        {
            var context = GetAppContext();
            var model = new ApiDocsPageModel(context);
            return View("~/Views/Pages/ApiDocs/ApiDocs.cshtml", model);
        }
    }
}