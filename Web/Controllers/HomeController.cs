using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.HomeModels;

namespace Web.Controllers
{
    public class HomeController : BaseController
    {
        [Route("remove")]
        public ActionResult Index()
        {
            var contextResult = GetBunchContext();
            var bunchListResult = UseCase.BunchList.Execute(new BunchList.UserBunchesRequest(Identity.UserName));
            var model = new HomePageModel(contextResult, bunchListResult);
            return View(model);
        }
    }
}