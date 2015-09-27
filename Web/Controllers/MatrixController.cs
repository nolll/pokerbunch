using System;
using System.Web.Mvc;
using Core.UseCases;
using Web.Common.Routes;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Matrix;
using Web.Urls;

namespace Web.Controllers
{
    public class MatrixController : BaseController
    {
        [Authorize]
        [Route(WebRoutes.CashgameMatrixWithYear)]
        public ActionResult Matrix(string slug, int? year = null)
        {
            var contextResult = GetCashgameContext(slug, DateTime.UtcNow, CashgameContext.CashgamePage.Matrix, year);
            var matrixResult = UseCase.Matrix.Execute(new Matrix.Request(CurrentUserName, slug, year));
            var model = new CashgameMatrixPageModel(contextResult, matrixResult);
            return View("~/Views/Pages/Matrix/MatrixPage.cshtml", model);
        }
    }
}