using System.Collections.Generic;
using Application.UseCases.BunchList;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IBunchListItemModelFactory
    {
        BunchListItemModel Create(BunchListItem bunchListItem);
        IList<BunchListItemModel> CreateList(IList<BunchListItem> bunchItems);
    }
}