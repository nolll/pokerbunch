using System.Collections.Generic;
using Core.UseCases;
using Core.UseCases.ShowBunchList;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IBunchListItemModelFactory
    {
        BunchListItemModel Create(BunchListItem bunchListItem);
        IList<BunchListItemModel> CreateList(IList<BunchListItem> bunchItems);
    }
}