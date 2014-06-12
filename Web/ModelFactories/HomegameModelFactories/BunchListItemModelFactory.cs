using System.Collections.Generic;
using System.Linq;
using Application.UseCases.BunchList;
using Web.Models.HomegameModels.List;
using Web.Models.UrlModels;

namespace Web.ModelFactories.HomegameModelFactories
{
    public class BunchListItemModelFactory : IBunchListItemModelFactory
    {
        public BunchListItemModel Create(BunchListItem bunchListItem)
        {
            return new BunchListItemModel
            {
                Name = bunchListItem.DisplayName,
                Url = new HomegameDetailsUrlModel(bunchListItem.Slug)
            };
        }

        public IList<BunchListItemModel> CreateList(IList<BunchListItem> bunchItems)
        {
            return bunchItems.Select(Create).ToList();
        }
    }
}