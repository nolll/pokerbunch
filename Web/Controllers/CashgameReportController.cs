using System.Web.Mvc;
using Core.UseCases.Report;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Report;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameReportController : PokerBunchController
    {
        [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/report")]
        public ActionResult Report_Post(string slug, ReportPostModel postModel)
        {
            var request = new ReportRequest(slug, postModel.PlayerId, postModel.Stack);
            UseCase.Report(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}