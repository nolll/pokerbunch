using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using Web.Controllers.Base;
using Web.Models.VueModels;

namespace Web.Controllers
{
    public class VueController : BaseController
    {
        public VueController(AppSettings appSettings)
            : base(appSettings)
        {
        }

        public ActionResult Root()
        {
            var model = new VuePageModel(AppSettings);
            return View(model);
        }
    }
}