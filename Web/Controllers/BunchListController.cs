using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.HomegameModels.List;
using Web.Routes;

namespace Web.Controllers
{
    public class BunchListController : BaseController
    {
        [Route(WebRoutes.Bunch.All)]
        public ActionResult All()
        {
            var context = GetAppContext();
            var bunchListResult = UseCase.BunchList.Execute(new BunchList.AllBunchesRequest(Identity.UserName));
            var model = new BunchListPageModel(context, bunchListResult);
            return View("~/Views/Pages/BunchList/BunchList.cshtml", model);
        }
    }
}