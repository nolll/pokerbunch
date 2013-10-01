using Core.Classes;
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

        public ForgotPasswordPageModel Create(User user)
        {
            return new ForgotPasswordPageModel
            {
                BrowserTitle = "Forgot Password",
                PageProperties = _pagePropertiesFactory.Create(user)
            };
        }

        public ForgotPasswordPageModel Create(User user, ForgotPasswordPostModel postModel)
        {
            var model = Create(user);
            model.Email = postModel.Email;
            return model;
        }

        public ForgotPasswordConfirmationPageModel CreateConfirmation(User user)
        {
            return new ForgotPasswordConfirmationPageModel
            {
                BrowserTitle = "Password Sent",
                PageProperties = _pagePropertiesFactory.Create(user)
            };
        }
    }
}