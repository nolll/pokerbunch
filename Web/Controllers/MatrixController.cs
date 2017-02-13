using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Matrix;
using Web.Routes;

namespace Web.Controllers
{
    public class MatrixController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.Cashgame.MatrixWithYear)]
        public ActionResult Matrix(string slug, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Matrix, year);
            var matrixResult = UseCase.Matrix.Execute(new Matrix.Request(slug, year));
            var model = new CashgameMatrixPageModel(contextResult, matrixResult);
            return View("~/Views/Pages/Matrix/MatrixPage.cshtml", model);
        }
    }
}