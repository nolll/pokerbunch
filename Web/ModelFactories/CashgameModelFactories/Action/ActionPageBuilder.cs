using Application.UseCases.Actions;
using Application.UseCases.BunchContext;
using Web.Models.CashgameModels.Action;

namespace Web.ModelFactories.CashgameModelFactories.Action
{
    public class ActionPageBuilder : IActionPageBuilder
    {
        private readonly IBunchContextInteractor _bunchContextInteractor;
        private readonly IActionsInteractor _actionsInteractor;

        public ActionPageBuilder(
            IBunchContextInteractor bunchContextInteractor,
            IActionsInteractor actionsInteractor)
        {
            _bunchContextInteractor = bunchContextInteractor;
            _actionsInteractor = actionsInteractor;
        }

        public ActionPageModel Build(string slug, string dateStr, int playerId)
        {
            var bunchContextResult = _bunchContextInteractor.Execute(new BunchContextRequest { Slug = slug });
            var actionsResult = _actionsInteractor.Execute(new ActionsRequest(slug, dateStr, playerId));

            return new ActionPageModel(bunchContextResult, actionsResult);
        }
    }
}