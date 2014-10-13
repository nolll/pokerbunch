using System.Web.Mvc;
using Core.Urls;
using Web.Commands.CashgameCommands;
using Web.Controllers.Base;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class DeleteCheckpointController : PokerBunchController
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public DeleteCheckpointController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeManager]
        [Route("{slug}/cashgame/deletecheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult DeleteCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var command = _cashgameCommandProvider.GetDeleteCheckpointCommand(slug, dateStr, checkpointId);
            if (command.Execute())
            {
                // if the cashgame isn't running, this should redirect back to the cashgame details
                return Redirect(new RunningCashgameUrl(slug).Relative);
            }
            var actionsUrl = new CashgameActionUrl(slug, dateStr, playerId);
            return Redirect(actionsUrl.Relative);
        }
    }
}