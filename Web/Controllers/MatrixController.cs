using System;
using System.Web.Mvc;
using Core.Urls;
using Core.UseCases;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Matrix;

namespace Web.Controllers
{
    public class MatrixController : BaseController
    {
        [Authorize]
        [Route(Routes.CashgameMatrixWithYear)]
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