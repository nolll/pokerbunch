using Application.Services;
using Application.UseCases.AppContext;
using Web.Models.AuthModels;

namespace Web.ModelFactories.AuthModelFactories
{
    public class LoginPageBuilder : ILoginPageBuilder
    {
        private readonly IWebContext _webContext;
        private readonly IAppContextInteractor _contextInteractor;

        public LoginPageBuilder(
            IWebContext webContext,
            IAppContextInteractor contextInteractor)
        {
            _webContext = webContext;
            _contextInteractor = contextInteractor;
        }

        public LoginPageModel Build(LoginPostModel postModel)
        {
            var returnUrl = _webContext.GetQueryParam("return");
            var contextResult = _contextInteractor.Execute();

            return new LoginPageModel(contextResult, returnUrl, postModel);
        }
    }
}