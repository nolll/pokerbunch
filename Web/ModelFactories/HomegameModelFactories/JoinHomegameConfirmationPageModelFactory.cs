using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Join;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class JoinHomegameConfirmationPageModelFactory : IJoinHomegameConfirmationPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public JoinHomegameConfirmationPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public JoinHomegameConfirmationPageModel Create(User user)
        {
            return new JoinHomegameConfirmationPageModel
                {
                    BrowserTitle = "Welcome",
                    PageProperties = _pagePropertiesFactory.Create(user)
                };
        }
    }
}