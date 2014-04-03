using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class AddHomegameConfirmationPageModelFactory : IAddHomegameConfirmationPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddHomegameConfirmationPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public AddHomegameConfirmationPageModel Create()
        {
            return new AddHomegameConfirmationPageModel
                {
                    BrowserTitle = "Homegame Created",
                    PageProperties = _pagePropertiesFactory.Create()
                };
        }
    }
}