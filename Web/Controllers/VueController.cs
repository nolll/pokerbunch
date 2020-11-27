using Core.Settings;
using Microsoft.AspNetCore.Mvc;
using Web.Models.VueModels;

namespace Web.Controllers
{
    public class VueController : Controller
    {
        private readonly AppSettings _appSettings;

        public VueController(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public ActionResult Root()
        {
            var model = new VuePageModel(_appSettings);
            return View(model.GetView(), model);
        }
    }
}