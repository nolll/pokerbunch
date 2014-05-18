using Core.Entities;
using Web.Models.HomegameModels.Details;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameDetailsPageModelFactory
    {
        HomegameDetailsPageModel Create(Homegame homegame, bool isInManagerMode);
    }
}