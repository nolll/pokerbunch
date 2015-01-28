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
            var contextResult = UseCase.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.Matrix));
            var matrixResult = UseCase.Matrix(new MatrixRequest(slug, year));
            var model = new CashgameMatrixPageModel(contextResult, matrixResult);
            return View("~/Views/Pages/Matrix/MatrixPage.cshtml", model);
        }
    }
}