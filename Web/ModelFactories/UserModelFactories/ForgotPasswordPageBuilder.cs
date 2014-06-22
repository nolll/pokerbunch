using Application.UseCases.AppContext;
using Web.Models.UserModels.ForgotPassword;

namespace Web.ModelFactories.UserModelFactories
{
    public class ForgotPasswordPageBuilder : IForgotPasswordPageBuilder
    {
        private readonly IAppContextInteractor _contextInteractor;

        public ForgotPasswordPageBuilder(IAppContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public ForgotPasswordPageModel Build(ForgotPasswordPostModel postModel)
        {
            var contextResult = _contextInteractor.Execute();

            var model = new ForgotPasswordPageModel(contextResult);

            if (postModel != null)
            {
                model.Email = postModel.Email;
            }

            return model;
        }

        public ForgotPasswordConfirmationPageModel BuildConfirmation()
        {
            var contextResult = _contextInteractor.Execute();

            return new ForgotPasswordConfirmationPageModel(contextResult);
        }
    }
}