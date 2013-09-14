using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.Listing;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class HomegameListingPageModelFactory : IHomegameListingPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public HomegameListingPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public HomegameListingPageModel Create(User user, IEnumerable<Homegame> homegames)
        {
            return new HomegameListingPageModel
                {
                    BrowserTitle = "Homegame List",
                    PageProperties = _pagePropertiesFactory.Create(user),
        	        HomegameModels = GetHomegameModels(homegames)
                };
        }

        private IList<HomegameListingItemModel> GetHomegameModels(IEnumerable<Homegame> homegames)
        {
            return homegames.Select(homegame => new HomegameListingItemModel(homegame)).ToList();
        }
    }
}