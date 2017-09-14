using Core.UseCases;
using Web.Extensions;
using Web.Urls.SiteUrls;

namespace Web.Models.HomegameModels.List
{
    public class BunchListItemModel : IViewModel
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public BunchListItemModel(BunchList.ResultItem bunchListItem)
        {
            Name = bunchListItem.DisplayName;
            Url = new BunchDetailsUrl(bunchListItem.Slug).Relative;
        }

        public View GetView()
        {
            return new View("BunchList/BunchListItem");
        }
    }
}