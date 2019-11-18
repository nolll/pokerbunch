using Core.UseCases;
using PokerBunch.Common.Urls.SiteUrls;
using Web.Extensions;

namespace Web.Models.HomegameModels.List
{
    public class BunchListItemModel : IViewModel
    {
        public string Name { get; }
        public string Url { get; }

        public BunchListItemModel(BunchList.ResultItem bunchListItem)
        {
            Name = bunchListItem.DisplayName;
            Url = new BunchDetailsUrl(bunchListItem.Slug).Relative;
        }

        public View GetView()
        {
            return new View("~/Views/Pages/BunchList/BunchListItem.cshtml");
        }
    }
}