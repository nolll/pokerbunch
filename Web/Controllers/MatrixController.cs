using System.Web.Mvc;
using Web.ModelFactories.CashgameModelFactories.Matrix;
using Web.Security.Attributes;
using ControllerBase = Web.Controllers.Base.ControllerBase;

namespace Web.Controllers
{
    public class MatrixController : ControllerBase
    {
        private readonly IMatrixPageBuilder _matrixPageBuilder;

        public MatrixController(IMatrixPageBuilder matrixPageBuilder)
        {
            _matrixPageBuilder = matrixPageBuilder;
        }

        [AuthorizePlayer]
        [Route("{slug}/cashgame/matrix/{year?}")]
        public ActionResult Matrix(string slug, int? year = null)
        {
            var model = _matrixPageBuilder.Build(slug, year);
            return View("Matrix/MatrixPage", model);
        }
    }
}