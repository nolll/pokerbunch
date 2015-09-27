using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Report;

namespace Web.Controllers
{
    public class CashgameReportController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(WebRoutes.CashgameReport)]
        public ActionResult Report_Post(string slug, ReportPostModel postModel)
        {
            var request = new Report.Request(CurrentUserName, slug, postModel.PlayerId, postModel.Stack, DateTime.UtcNow);
            UseCase.Report.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}