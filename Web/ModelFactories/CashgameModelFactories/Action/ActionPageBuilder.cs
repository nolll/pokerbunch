using System.Collections.Generic;
using System.Linq;
using Application.UseCases.Actions;
using Application.UseCases.CashgameContext;
using Web.Models.CashgameModels.Action;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public class ActionPageBuilder : IActionPageBuilder
    {
        private readonly ICashgameContextInteractor _cashgameContextInteractor;
        private readonly IActionsInteractor _actionsInteractor;

        public ActionPageBuilder(
            ICashgameContextInteractor cashgameContextInteractor,
            IActionsInteractor actionsInteractor)
        {
            _cashgameContextInteractor = cashgameContextInteractor;
            _actionsInteractor = actionsInteractor;
        }

        public ActionPageModel Build(string slug, string dateStr, int playerId)
        {
            var cashgameContextResult = _cashgameContextInteractor.Execute(new CashgameContextRequest{Slug = slug});
            var actionsResult = _actionsInteractor.Execute(new ActionsRequest(slug, dateStr, playerId));

            var heading = string.Format("Cashgame {0}, {1}", actionsResult.Date, actionsResult.PlayerName);
            
            return new ActionPageModel
                {
                    BrowserTitle = "Player Actions",
                    PageProperties = new PageProperties(cashgameContextResult),
                    Heading = heading,
                    Checkpoints = GetCheckpointModels(actionsResult),
                    ChartDataUrl = actionsResult.ChartDataUrl
                };
        }

        private List<CheckpointModel> GetCheckpointModels(ActionsResult actionsResult)
        {
            return actionsResult.CheckpointItems.Select(o => new CheckpointModel(o)).ToList();
        }
    }
}