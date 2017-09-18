using System.Collections.Generic;
using Web.Urls.SiteUrls;

namespace Web.Components.ApiDocsModels
{
    public class DocsNavigationBlockModel : DocsBlockModel
    {
        public IEnumerable<DocsNavigationItemModel> Items { get; }

        public DocsNavigationBlockModel()
        {
            Items = new[]
            {
                new DocsNavigationItemModel("Authentication", new ApiDocsAuthUrl().Relative),
                new DocsNavigationItemModel("Bunches", new ApiDocsBunchesUrl().Relative)
            };
        }
    }
}