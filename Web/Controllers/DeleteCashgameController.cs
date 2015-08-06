using System.Web.Mvc;
using Core.UseCases.DeleteCashgame;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCashgameController : BaseController
    {
        [Authorize]
        [Route("{slug}/cashgame/delete/{id}")]
        public ActionResult Delete(string slug, int id)
        {
            var context = GetBunchContext(slug);
            RequireManager(context);
            var request = new DeleteCashgameRequest(slug, id);
            var result = UseCase.DeleteCashgame.Execute(request);
            return Redirect(result.ReturnUrl.Relative);
		}
    }
}