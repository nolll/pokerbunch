using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Matrix;

namespace Web.Controllers
{
    public class MatrixController : BaseController
    {
        [Authorize]
        [Route("{slug}/cashgame/matrix/{year?}")]
        public ActionResult Matrix(string slug, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Matrix, year);
            RequirePlayer(contextResult.BunchContext);
            var matrixResult = UseCase.Matrix.Execute(new Matrix.Request(slug, year));
            var model = new CashgameMatrixPageModel(contextResult, matrixResult);
            return View("~/Views/Pages/Matrix/MatrixPage.cshtml", model);
        }
    }
}