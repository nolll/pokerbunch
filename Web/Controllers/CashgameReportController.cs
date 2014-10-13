using System.Web.Mvc;
using Core.Urls;
using Core.UseCases.BunchContext;
using Web.Commands.CashgameCommands;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Report;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class CashgameReportController : PokerBunchController
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public CashgameReportController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/report/{playerId:int}")]
        public ActionResult Report(string slug, int playerId)
        {
            var model = BuildReportModel(slug);
            return View("~/Views/Pages/CashgameReport/Report.cshtml", model);
        }

        [HttpPost]
        [AuthorizeOwnPlayer]
        [Route("{slug}/cashgame/report/{playerId:int}")]
        public ActionResult Report_Post(string slug, int playerId, ReportPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetReportCommand(slug, playerId, postModel);
            if (command.Execute())
            {
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildReportModel(slug, postModel);
            return View("~/Views/Pages/CashgameReport/Report.cshtml", model);
        }

        private ReportPageModel BuildReportModel(string slug, ReportPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            return new ReportPageModel(contextResult, postModel);
        }
    }
}