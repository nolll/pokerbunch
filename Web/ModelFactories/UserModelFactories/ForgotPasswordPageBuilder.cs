using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.ForgotPassword;

namespace Web.ModelFactories.UserModelFactories
{
    public class ForgotPasswordPageBuilder : IForgotPasswordPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public ForgotPasswordPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        private ForgotPasswordPageModel Create()
        {
            return new ForgotPasswordPageModel
            {
                BrowserTitle = "Forgot Password",
                PageProperties = _pagePropertiesFactory.Create()
            };
        }

        public ForgotPasswordPageModel Build(ForgotPasswordPostModel postModel)
        {
            var model = Create();
            if (postModel != null)
            {
                model.Email = postModel.Email;
            }
            return model;
        }

        public ForgotPasswordConfirmationPageModel BuildConfirmation()
        {
            return new ForgotPasswordConfirmationPageModel
            {
                BrowserTitle = "Password Sent",
                PageProperties = _pagePropertiesFactory.Create()
            };
        }
    }
}