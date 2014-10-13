using System.Web.Mvc;
using Web.Controllers.Base;
using Web.Models.HomegameModels.List;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class BunchListController : PokerBunchController
    {
        [AuthorizeAdmin]
        [Route("-/homegame/list")]
        public ActionResult List()
        {
            var contextResult = UseCase.AppContext();
            var bunchListResult = UseCase.BunchList();
            var model = new BunchListPageModel(contextResult, bunchListResult);
            return View("~/Views/Pages/BunchList/BunchList.cshtml", model);
        }
    }
}