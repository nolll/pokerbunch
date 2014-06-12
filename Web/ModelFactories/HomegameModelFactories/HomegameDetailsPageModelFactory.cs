using Application.Services;
using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Details;
using Web.Models.UrlModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class HomegameDetailsPageModelFactory : IHomegameDetailsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;

        public HomegameDetailsPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
        }

        public HomegameDetailsPageModel Create(Homegame homegame, bool isInManagerMode)
        {
            var houseRules = FormatHouseRules(homegame.HouseRules);

            return new HomegameDetailsPageModel
                {
                    BrowserTitle = "Homegame Details",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
	                DisplayName = homegame.DisplayName,
			        Description = homegame.Description,
			        HouseRules = houseRules,
	                ShowHouseRules = !string.IsNullOrEmpty(houseRules),
			        EditUrl = new EditHomegameUrlModel(homegame.Slug),
			        ShowEditLink = isInManagerMode
                };
        }

        private string FormatHouseRules(string houseRules)
        {
            return houseRules != null ? houseRules.Trim().Replace("\n\r", "<br />\n\r") : null;
        }
    }
}