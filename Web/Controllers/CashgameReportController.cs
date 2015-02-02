using System;
using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.Report;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Report;

namespace Web.Controllers
{
    public class CashgameReportController : PokerBunchController
    {
        [HttpPost]
        [Authorize]
        [Route("{slug}/cashgame/report")]
        public ActionResult Report_Post(string slug, ReportPostModel postModel)
        {
            if (!IsPlayer(slug, postModel.PlayerId))
                throw new AccessDeniedException();
            var request = new ReportRequest(slug, postModel.PlayerId, postModel.Stack, DateTime.UtcNow);
            UseCase.Report.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}