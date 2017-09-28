using System;
using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Matrix;

namespace Web.Controllers
{
    public class MatrixController : BaseController
    {
        [Authorize]
        [Route(MatrixUrl.RouteWithYear)]
        public ActionResult Matrix(string bunchId, int? year = null)
        {
            var contextResult = GetCashgameContext(bunchId, DateTime.UtcNow, CashgameContext.CashgamePage.Matrix, year);
            var matrixResult = UseCase.BunchMatrix.Execute(new BunchMatrix.Request(bunchId, year));
            var model = new CashgameMatrixPageModel(contextResult, matrixResult);
            return View(model);
        }
    }
}