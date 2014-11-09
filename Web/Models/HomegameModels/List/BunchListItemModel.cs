using Core.UseCases.BunchList;

namespace Web.Models.HomegameModels.List
{
    public class BunchListItemModel
    {
        public string Name { get; private set; }
        public string Url { get; private set; }

        public BunchListItemModel(BunchListItem bunchListItem)
        {
            Name = bunchListItem.DisplayName;
            Url = bunchListItem.Url.Relative;
        }
    }
}