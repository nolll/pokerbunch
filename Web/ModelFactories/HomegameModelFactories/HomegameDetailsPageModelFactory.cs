using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Details;
using Web.Models.UrlModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class HomegameDetailsPageModelFactory : IHomegameDetailsPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public HomegameDetailsPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public HomegameDetailsPageModel Create(User user, Homegame homegame, bool isInManagerMode, Cashgame runningGame = null)
        {
            var houseRules = FormatHouseRules(homegame.HouseRules);

            return new HomegameDetailsPageModel
                {
                    BrowserTitle = "Homegame Details",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame, runningGame),
	                DisplayName = homegame.DisplayName,
			        Description = homegame.Description,
			        HouseRules = houseRules,
	                ShowHouseRules = !string.IsNullOrEmpty(houseRules),
			        EditUrl = new HomegameEditUrlModel(homegame),
			        ShowEditLink = isInManagerMode
                };
        }

        private string FormatHouseRules(string houseRules)
        {
            return houseRules != null ? houseRules.Trim().Replace("\n\r", "<br />\n\r") : null;
        }
    }
}