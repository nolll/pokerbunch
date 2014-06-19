using Application.UseCases.AppContext;
using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public class AddUserConfirmationPageBuilder : IAddUserConfirmationPageBuilder
    {
        private readonly IAppContextInteractor _contextInteractor;

        public AddUserConfirmationPageBuilder(IAppContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public AddUserConfirmationPageModel Build()
        {
            var contextResult = _contextInteractor.Execute();

            return new AddUserConfirmationPageModel(contextResult);
        }
    }
}