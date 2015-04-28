using System;
using System.Web.Mvc;
using Core.UseCases.CashgameContext;
using Core.UseCases.Matrix;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Matrix;

namespace Web.Controllers
{
    public class MatrixController : PokerBunchController
    {
        [Authorize]
        [Route("{slug}/cashgame/matrix/{year?}")]
        public ActionResult Matrix(string slug, int? year = null)
        {
            RequirePlayer(slug);
            var contextResult = UseCase.CashgameContext.Execute(new CashgameContextRequest(slug, DateTime.UtcNow, CashgamePage.Matrix, year));
            var matrixResult = UseCase.Matrix.Execute(new MatrixRequest(slug, year));
            var model = new CashgameMatrixPageModel(contextResult, matrixResult);
            return View("~/Views/Pages/Matrix/MatrixPage.cshtml", model);
        }
    }
}