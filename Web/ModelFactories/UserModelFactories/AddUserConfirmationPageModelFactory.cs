using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public class AddUserConfirmationPageModelFactory : IAddUserConfirmationPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddUserConfirmationPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public AddUserConfirmationPageModel Create()
        {
            return new AddUserConfirmationPageModel
                {
                    BrowserTitle = "Homegame Created",
                    PageProperties = _pagePropertiesFactory.Create()
                };
        }
    }
}