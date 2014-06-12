using Application.UseCases.BunchList;
using Web.Models.UrlModels;

namespace Web.Models.HomegameModels.List
{
    public class BunchListItemModel
    {
        public string Name { get; private set; }
        public Url Url { get; private set; }

        public BunchListItemModel(BunchListItem bunchListItem)
        {
            Name = bunchListItem.DisplayName;
            Url = new HomegameDetailsUrl(bunchListItem.Slug);
        }
    }
}