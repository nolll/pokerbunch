using System.Web.Mvc;
using Core.UseCases.BunchContext;
using Core.UseCases.BunchList;
using Web.Controllers.Base;
using Web.Models.HomeModels;

namespace Web.Controllers
{
    public class HomeController : PokerBunchController
    {
        [Route("")]
        public ActionResult Index()
        {
            var contextResult = UseCase.BunchContext.Execute(new BunchContextRequest());
            var bunchListResult = UseCase.BunchList.Execute(new BunchListRequest(Identity.UserId));
            var homeResult = UseCase.Home.Execute();
            var model = new HomePageModel(contextResult, homeResult, bunchListResult);
            return View("~/Views/Pages/Home/Index.cshtml", model);
        }
    }
}