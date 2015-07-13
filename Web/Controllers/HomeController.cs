using System.Web.Mvc;
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
            var contextResult = GetBunchContext();
            var bunchListResult = UseCase.BunchList.Execute(new BunchListRequest(CurrentUserName));
            var model = new HomePageModel(contextResult, bunchListResult);
            return View("~/Views/Pages/Home/Index.cshtml", model);
        }
    }
}