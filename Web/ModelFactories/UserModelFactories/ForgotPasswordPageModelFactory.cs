using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.ForgotPassword;

namespace Web.ModelFactories.UserModelFactories
{
    public class ForgotPasswordPageModelFactory : IForgotPasswordPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public ForgotPasswordPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
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

        public ForgotPasswordPageModel Create(ForgotPasswordPostModel postModel)
        {
            var model = Create();
            if (postModel != null)
            {
                model.Email = postModel.Email;
            }
            return model;
        }

        public ForgotPasswordConfirmationPageModel CreateConfirmation()
        {
            return new ForgotPasswordConfirmationPageModel
            {
                BrowserTitle = "Password Sent",
                PageProperties = _pagePropertiesFactory.Create()
            };
        }
    }
}