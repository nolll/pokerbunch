using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Add;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class AddHomegameConfirmationPageBuilder : IAddHomegameConfirmationPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public AddHomegameConfirmationPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public AddHomegameConfirmationPageModel Build()
        {
            return new AddHomegameConfirmationPageModel
                {
                    BrowserTitle = "Homegame Created",
                    PageProperties = _pagePropertiesFactory.Create()
                };
        }
    }
}