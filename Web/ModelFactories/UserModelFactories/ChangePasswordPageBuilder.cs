using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.ChangePassword;

namespace Web.ModelFactories.UserModelFactories
{
    public class ChangePasswordPageBuilder : IChangePasswordPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public ChangePasswordPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public ChangePasswordPageModel Build()
        {
            return new ChangePasswordPageModel
                {
                    BrowserTitle = "Change Password",
                    PageProperties = _pagePropertiesFactory.Create()
                };
        }

        public ChangePasswordConfirmationPageModel BuildConfirmation()
        {
            return new ChangePasswordConfirmationPageModel
            {
                BrowserTitle = "Password Changed",
                PageProperties = _pagePropertiesFactory.Create()
            };
        }
    }
}