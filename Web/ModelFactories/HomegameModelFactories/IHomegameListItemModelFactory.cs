using Core.Classes;
using Web.Models.HomegameModels.List;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameListItemModelFactory
    {
        HomegameListItemModel Create(Homegame homegame);
    }
}