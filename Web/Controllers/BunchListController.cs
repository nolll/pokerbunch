using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.HomegameModels.List;
using Web.Routes;

namespace Web.Controllers
{
    public class BunchListController : BaseController
    {
        [Route(WebRoutes.Bunch.All)]
        public ActionResult All()
        {
            var context = GetAppContext();
            var bunchListResult = UseCase.BunchList.Execute();
            var model = new BunchListPageModel(context, bunchListResult);
            return View(model);
        }
    }
}