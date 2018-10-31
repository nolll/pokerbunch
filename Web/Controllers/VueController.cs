using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.VueModels;

namespace Web.Controllers
{
    public class VueController : BaseController
    {
        public ActionResult Root()
        {
            var baseContext = GetBaseContext();
            var model = new VuePageModel(baseContext);
            return View(model);
        }
    }
}