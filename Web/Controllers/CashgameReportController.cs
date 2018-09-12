using System.Web.Http;
using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Report;

namespace Web.Controllers
{
    public class CashgameReportController : BaseController
    {
        [System.Web.Mvc.HttpPost]
        [System.Web.Mvc.Authorize]
        [System.Web.Mvc.Route(CashgameReportUrl.Route)]
        public ActionResult Report_Post(string bunchId, ReportPostModel postModel)
        {
            var request = new Report.Request(bunchId, postModel.PlayerId, postModel.Stack);
            UseCase.Report.Execute(request);
            return JsonView(new JsonViewModelOk());
        }
    }
}