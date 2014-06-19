using Application.UseCases.Actions;
using Application.UseCases.BunchContext;
using Web.Models.CashgameModels.Action;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public class ActionPageBuilder : IActionPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;
        private readonly IActionsInteractor _actionsInteractor;

        public ActionPageBuilder(
            IBunchContextInteractor contextInteractor,
            IActionsInteractor actionsInteractor)
        {
            _contextInteractor = contextInteractor;
            _actionsInteractor = actionsInteractor;
        }

        public ActionPageModel Build(string slug, string dateStr, int playerId)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest { Slug = slug });
            var actionsResult = _actionsInteractor.Execute(new ActionsRequest(slug, dateStr, playerId));

            return new ActionPageModel(contextResult, actionsResult);
        }
    }
}