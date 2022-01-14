using Microsoft.AspNetCore.Mvc;
using Web.Models.VueModels;
using Web.Settings;

namespace Web.Controllers;

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