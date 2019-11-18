using Core.Settings;
using Core.UseCases;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Controllers.Base;

namespace Web.Controllers
{
    public class DeleteCashgameController : BaseController
    {
        private readonly DeleteCashgame _deleteCashgame;

        public DeleteCashgameController(AppSettings appSettings, DeleteCashgame deleteCashgame) 
            : base(appSettings)
        {
            _deleteCashgame = deleteCashgame;
        }

        [Authorize]
        [Route(DeleteCashgameUrl.Route)]
        public ActionResult Delete(string cashgameId)
        {
            var request = new DeleteCashgame.Request(cashgameId);
            var result = _deleteCashgame.Execute(request);
            return Redirect(new CashgameIndexUrl(result.Slug).Relative);
		}
    }
}