using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public class AddUserConfirmationPageBuilder : IAddUserConfirmationPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddUserConfirmationPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public AddUserConfirmationPageModel Build()
        {
            return new AddUserConfirmationPageModel
                {
                    BrowserTitle = "Homegame Created",
                    PageProperties = _pagePropertiesFactory.Create()
                };
        }
    }
}