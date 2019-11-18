using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.HomegameModels.List;

namespace Web.Controllers
{
    public class BunchListController : CoreController
    {
        private readonly BunchList _bunchList;

        public BunchListController(AppSettings appSettings, CoreContext coreContext, BunchList bunchList) 
            : base(appSettings, coreContext)
        {
            _bunchList = bunchList;
        }

        [Route(BunchListAllUrl.Route)]
        public ActionResult All()
        {
            var context = GetAppContext();
            var bunchListResult = _bunchList.Execute();
            var model = new BunchListPageModel(AppSettings, context, bunchListResult);
            return View(model);
        }
    }
}