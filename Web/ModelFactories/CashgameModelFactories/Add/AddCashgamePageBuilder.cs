using Application.UseCases.AddCashgameForm;
using Application.UseCases.BunchContext;
using Web.Models.CashgameModels.Add;

namespace Web.ModelFactories.CashgameModelFactories.Add
{
    public class AddCashgamePageBuilder : IAddCashgamePageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;
        private readonly IAddCashgameFormInteractor _addCashgameFormInteractor;

        public AddCashgamePageBuilder(
            IBunchContextInteractor contextInteractor,
            IAddCashgameFormInteractor addCashgameFormInteractor)
        {
            _contextInteractor = contextInteractor;
            _addCashgameFormInteractor = addCashgameFormInteractor;
        }

        public AddCashgamePageModel Build(string slug, AddCashgamePostModel postModel)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));
            var optionsResult = _addCashgameFormInteractor.Execute(new AddCashgameFormRequest(slug));

            return new AddCashgamePageModel(contextResult, optionsResult, postModel);
        } 
    }
}