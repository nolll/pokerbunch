using System;
using System.Web.Mvc;
using Core.UseCases;
using PokerBunch.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Matrix;

namespace Web.Controllers
{
    public class MatrixController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.MatrixWithYear)]
        public ActionResult Matrix(string slug, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Matrix, year);
            var matrixResult = UseCase.BunchMatrix.Execute(new BunchMatrix.Request(slug, year));
            var model = new CashgameMatrixPageModel(contextResult, matrixResult);
            return View(model);
        }
    }
}