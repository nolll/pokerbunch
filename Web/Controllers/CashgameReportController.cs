using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Report;
using Web.Urls;

namespace Web.Controllers
{
    public class CashgameReportController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(Routes.CashgameReport)]
        public ActionResult Report_Post(string slug, ReportPostModel postModel)
        {
            var request = new Report.Request(CurrentUserName, slug, postModel.PlayerId, postModel.Stack, DateTime.UtcNow);
            var result = UseCase.Report.Execute(request);
            Buster.CashgameUpdated(result.CashgameId);
            return JsonView(new JsonViewModelOk());
        }
    }
}