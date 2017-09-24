using System.Web.Mvc;
using PokerBunch.Common.Routes;
using Web.Controllers.Base;
using Web.Models.HomegameModels.List;

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