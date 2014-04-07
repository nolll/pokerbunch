using System.Collections.Generic;
using Core.UseCases;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IBunchListItemModelFactory
    {
        BunchListItemModel Create(BunchItem bunchItem);
        IList<BunchListItemModel> CreateList(IList<BunchItem> bunchItems);
    }
}