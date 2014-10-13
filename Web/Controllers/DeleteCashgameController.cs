using System.Web.Mvc;
using Core.Urls;
using Web.Commands.CashgameCommands;
using Web.Controllers.Base;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class DeleteCashgameController : PokerBunchController
    {
	    private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public DeleteCashgameController(ICashgameCommandProvider cashgameCommandProvider)
	    {
	        _cashgameCommandProvider = cashgameCommandProvider;
	    }

        [AuthorizeManager]
        [Route("{slug}/cashgame/delete/{dateStr}")]
        public ActionResult Delete(string slug, string dateStr)
        {
            var command = _cashgameCommandProvider.GetDeleteCommand(slug, dateStr);
            command.Execute();
            return Redirect(new CashgameIndexUrl(slug).Relative);
		}
    }
}