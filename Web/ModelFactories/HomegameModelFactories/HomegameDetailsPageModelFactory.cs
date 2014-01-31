using Application.Services;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Details;

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

        public HomegameDetailsPageModel Create(User user, Homegame homegame, bool isInManagerMode)
        {
            var houseRules = FormatHouseRules(homegame.HouseRules);

            return new HomegameDetailsPageModel
                {
                    BrowserTitle = "Homegame Details",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
	                DisplayName = homegame.DisplayName,
			        Description = homegame.Description,
			        HouseRules = houseRules,
	                ShowHouseRules = !string.IsNullOrEmpty(houseRules),
			        EditUrl = _urlProvider.GetHomegameEditUrl(homegame.Slug),
			        ShowEditLink = isInManagerMode
                };
        }

        private string FormatHouseRules(string houseRules)
        {
            return houseRules != null ? houseRules.Trim().Replace("\n\r", "<br />\n\r") : null;
        }
    }
}