using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.ApiDocsModels;
using Web.Routes;

namespace Web.Controllers
{
    public class ApiDocsController : BaseController
    {
        [Route(WebRoutes.Api.DocsIndex)]
        public ActionResult Index()
        {
            var context = GetAppContext();
            var model = new ApiDocsIndexPageModel(context);
            return View(model);
        }

        [Route(WebRoutes.Api.DocsAuth)]
        public ActionResult Auth()
        {
            var context = GetAppContext();
            var model = new ApiDocsAuthPageModel(context);
            return View(model);
        }

        [Route(WebRoutes.Api.DocsBunches)]
        public ActionResult Bunches()
        {
            var context = GetAppContext();
            var model = new ApiDocsBunchesPageModel(context);
            return View(model);
        }
    }
}