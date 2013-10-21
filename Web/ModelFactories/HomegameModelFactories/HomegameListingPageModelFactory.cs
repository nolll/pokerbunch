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
        private readonly IHomegameListingItemModelFactory _homegameListingItemModelFactory;

        public HomegameListingPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameListingItemModelFactory homegameListingItemModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameListingItemModelFactory = homegameListingItemModelFactory;
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
            return homegames.Select(homegame => _homegameListingItemModelFactory.Create(homegame)).ToList();
        }
    }
}