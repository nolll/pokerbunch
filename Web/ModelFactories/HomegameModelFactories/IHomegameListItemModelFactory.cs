using Core.UseCases;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameListItemModelFactory
    {
        BunchListItemModel Create(BunchItem bunchItem);
    }
}