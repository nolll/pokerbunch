using System.Collections.Generic;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Components.ApiDocsModels.Block;

namespace Web.Components.ApiDocsModels.NavigationBlock
{
    public class NavigationBlockModel : DocsBlockModel
    {
        public IEnumerable<NavigationItemModel> Items { get; }

        public NavigationBlockModel()
        {
            Items = new[]
            {
                new NavigationItemModel("Authentication", new ApiDocsAuthUrl().Relative),
                new NavigationItemModel("Bunches", new ApiDocsBunchesUrl().Relative),
                new NavigationItemModel("Cashgames", new ApiDocsCashgamesUrl().Relative)
            };
        }
    }
}