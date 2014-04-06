using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
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

        public HomegameListPageModel Create(ShowBunchListResult showBunchListResult)
        {
            return new HomegameListPageModel
            {
                BrowserTitle = "Homegame List",
                PageProperties = _pagePropertiesFactory.Create(),
                HomegameModels = GetHomegameModels(showBunchListResult.Bunches)
            };
        }

        private IList<HomegameListItemModel> GetHomegameModels(IEnumerable<BunchItem> bunchItems)
        {
            return bunchItems.Select(o => _homegameListItemModelFactory.Create(o)).ToList();
        }
    }
}