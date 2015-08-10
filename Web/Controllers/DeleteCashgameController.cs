using System.Web.Mvc;
using Core.UseCases;
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
            var request = new DeleteCashgame.Request(slug, id);
            var result = UseCase.DeleteCashgame.Execute(request);
            return Redirect(result.ReturnUrl.Relative);
		}
    }
}