using System.Web.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.HomegameModels.List;

namespace Web.Controllers
{
    public class BunchListController : BaseController
    {
        [Route(BunchListAllUrl.Route)]
        public ActionResult All()
        {
            var context = GetAppContext();
            var bunchListResult = UseCase.BunchList.Execute();
            var model = new BunchListPageModel(context, bunchListResult);
            return View(model);
        }
    }
}