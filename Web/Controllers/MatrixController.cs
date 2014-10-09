using System.Web.Mvc;
using Core.UseCases.CashgameContext;
using Core.UseCases.Matrix;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Matrix;
using Web.Plumbing;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class MatrixController : PokerBunchController
    {
        [AuthorizePlayer]
        [Route("{slug}/cashgame/matrix/{year?}")]
        public ActionResult Matrix(string slug, int? year = null)
        {
            var contextResult = UseCaseContainer.Instance.CashgameContext(new CashgameContextRequest(slug, year, CashgamePage.Matrix));
            var matrixResult = UseCaseContainer.Instance.Matrix(new MatrixRequest(slug, year));
            var model = new CashgameMatrixPageModel(contextResult, matrixResult);
            return View("Matrix/MatrixPage", model);
        }
    }
}