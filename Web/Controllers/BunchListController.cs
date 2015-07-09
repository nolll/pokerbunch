using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.HomegameModels.List;

namespace Web.Controllers
{
    public class BunchListController : PokerBunchController
    {
        [Route("-/homegame/list")]
        public ActionResult List()
        {
            RequireAdmin();
            var contextResult = GetAppContext();
            var bunchListResult = UseCase.BunchList.Execute();
            var model = new BunchListPageModel(contextResult, bunchListResult);
            return View("~/Views/Pages/BunchList/BunchList.cshtml", model);
        }
    }
}