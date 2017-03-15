using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Report;
using Web.Routes;

namespace Web.Controllers
{
    public class CashgameReportController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(WebRoutes.Cashgame.Report)]
        public ActionResult Report_Post(string slug, ReportPostModel postModel)
        {
            var request = new Report.Request(slug, postModel.PlayerId, postModel.Stack);
            UseCase.Report.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}