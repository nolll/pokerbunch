using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class HomegameListPageModelFactory : IHomegameListPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameListItemModelFactory _homegameListItemModelFactory;

        public HomegameListPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameListItemModelFactory homegameListItemModelFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameListItemModelFactory = homegameListItemModelFactory;
        }

        public HomegameListPageModel Create(User user, IEnumerable<Homegame> homegames)
        {
            return new HomegameListPageModel
                {
                    BrowserTitle = "Homegame List",
                    PageProperties = _pagePropertiesFactory.Create(user),
        	        HomegameModels = GetHomegameModels(homegames)
                };
        }

        private IList<HomegameListItemModel> GetHomegameModels(IEnumerable<Homegame> homegames)
        {
            return homegames.Select(homegame => _homegameListItemModelFactory.Create(homegame)).ToList();
        }
    }
}