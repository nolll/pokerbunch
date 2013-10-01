using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.ChangePassword;

namespace Web.ModelFactories.UserModelFactories
{
    public class ChangePasswordPageModelFactory : IChangePasswordPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public ChangePasswordPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public ChangePasswordPageModel Create(User user)
        {
            return new ChangePasswordPageModel
                {
                    BrowserTitle = "Change Password",
                    PageProperties = _pagePropertiesFactory.Create(user)
                };
        }

        public ChangePasswordConfirmationPageModel CreateConfirmation(User user)
        {
            return new ChangePasswordConfirmationPageModel
            {
                BrowserTitle = "Password Changed",
                PageProperties = _pagePropertiesFactory.Create(user)
            };
        }
    }
}