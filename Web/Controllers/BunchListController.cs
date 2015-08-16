using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.HomegameModels.List;

namespace Web.Controllers
{
    public class BunchListController : BaseController
    {
        [Route(Routes.BunchList)]
        public ActionResult List()
        {
            var context = GetAppContext();
            var bunchListResult = UseCase.BunchList.Execute(new BunchList.AllBunchesRequest(CurrentUserName));
            var model = new BunchListPageModel(context, bunchListResult);
            return View("~/Views/Pages/BunchList/BunchList.cshtml", model);
        }
    }
}