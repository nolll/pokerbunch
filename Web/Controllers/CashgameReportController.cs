using System.Web.Mvc;
using Core.Exceptions;
using Core.UseCases.BunchContext;
using Core.UseCases.Report;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Report;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameReportController : PokerBunchController
    {
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/report/{playerId:int}")]
        public ActionResult Report(string slug, int playerId)
        {
            return ShowForm(slug);
        }

        [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/report/{playerId:int}")]
        public ActionResult Report_Post(string slug, int playerId, ReportPostModel postModel)
        {
            try
            {
                var request = new ReportRequest(slug, playerId, postModel.StackAmount);
                var result = UseCase.Report(request);
                return Redirect(result.ReturnUrl.Relative);
            }
            catch (ValidationException ex)
            {
                AddModelErrors(ex.Messages);
            }
            return ShowForm(slug, postModel);
        }

        private ActionResult ShowForm(string slug, ReportPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var model = new ReportPageModel(contextResult, postModel);
            return View("~/Views/Pages/CashgameReport/Report.cshtml", model);
        }
    }
}