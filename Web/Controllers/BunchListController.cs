using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.HomegameModels.List;

namespace Web.Controllers
{
    public class BunchListController : BaseController
    {
        [Route(WebRoutes.BunchList)]
        public ActionResult List()
        {
            var context = GetAppContext();
            var bunchListResult = UseCase.BunchList.Execute(new BunchList.AllBunchesRequest(CurrentUserName));
            var model = new BunchListPageModel(context, bunchListResult);
            return View("~/Views/Pages/BunchList/BunchList.cshtml", model);
        }
    }
}