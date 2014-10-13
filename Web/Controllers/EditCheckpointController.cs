using System.Web.Mvc;
using Core.Urls;
using Core.UseCases.BunchContext;
using Core.UseCases.EditCheckpointForm;
using Web.Commands.CashgameCommands;
using Web.Controllers.Base;
using Web.Models.CashgameModels.Checkpoints;
using Web.Security.Attributes;

namespace Web.Controllers
{
    public class EditCheckpointController : PokerBunchController
    {
        private readonly ICashgameCommandProvider _cashgameCommandProvider;

        public EditCheckpointController(ICashgameCommandProvider cashgameCommandProvider)
        {
            _cashgameCommandProvider = cashgameCommandProvider;
        }

        [AuthorizeManager]
        [Route("{slug}/cashgame/editcheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult EditCheckpoint(string slug, string dateStr, int playerId, int checkpointId)
        {
            var model = BuildEditCheckpointModel(slug, dateStr, playerId, checkpointId);
            return View("~/Views/Pages/EditCheckpoint/Edit.cshtml", model);
        }

        [HttpPost]
        [AuthorizeManager]
        [Route("{slug}/cashgame/editcheckpoint/{dateStr}/{playerId:int}/{checkpointId:int}")]
        public ActionResult EditCheckpoint_Post(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel)
        {
            var command = _cashgameCommandProvider.GetEditCheckpointCommand(slug, dateStr, checkpointId, postModel);
            if (command.Execute())
            {
                return Redirect(new CashgameActionUrl(slug, dateStr, playerId).Relative);
            }
            AddModelErrors(command.Errors);
            var model = BuildEditCheckpointModel(slug, dateStr, playerId, checkpointId, postModel);
            return View("~/Views/Pages/EditCheckpoint/Edit.cshtml", model);
        }

        private EditCheckpointPageModel BuildEditCheckpointModel(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel = null)
        {
            var contextResult = UseCase.BunchContext(new BunchContextRequest(slug));
            var editCheckpointFormResult = UseCase.EditCheckpointForm(new EditCheckpointFormRequest(slug, dateStr, playerId, checkpointId));
            return new EditCheckpointPageModel(contextResult, editCheckpointFormResult, postModel);
        }
    }
}