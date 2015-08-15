using System;
using System.Web.Mvc;
using Core.Exceptions;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Report;

namespace Web.Controllers
{
    public class CashgameReportController : BaseController
    {
        [HttpPost]
        [Authorize]
        [Route(Routes.CashgameReport)]
        public ActionResult Report_Post(string slug, ReportPostModel postModel)
        {
            var bunchContext = GetBunchContext(slug);
            if (!bunchContext.IsCurrentPlayer(postModel.PlayerId))
                throw new AccessDeniedException();
            var request = new Report.Request(slug, postModel.PlayerId, postModel.Stack, DateTime.UtcNow);
            UseCase.Report.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}