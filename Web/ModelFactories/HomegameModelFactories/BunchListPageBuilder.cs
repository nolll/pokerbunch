using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class BunchListPageBuilder : IBunchListPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameListItemModelFactory _homegameListItemModelFactory;
        private readonly IShowBunchList _showBunchList;

        public BunchListPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameListItemModelFactory homegameListItemModelFactory,
            IShowBunchList showBunchList)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameListItemModelFactory = homegameListItemModelFactory;
            _showBunchList = showBunchList;
        }

        public BunchListPageModel Create()
        {
            var result = _showBunchList.Execute();

            return new BunchListPageModel
            {
                BrowserTitle = "Bunches",
                PageProperties = _pagePropertiesFactory.Create(),
                BunchModels = GetHomegameModels(result.Bunches)
            };
        }

        private IList<BunchListItemModel> GetHomegameModels(IEnumerable<BunchItem> bunchItems)
        {
            return bunchItems.Select(o => _homegameListItemModelFactory.Create(o)).ToList();
        }
    }
}