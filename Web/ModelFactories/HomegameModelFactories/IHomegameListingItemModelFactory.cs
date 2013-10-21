using Core.Classes;
using Web.Models.HomegameModels.Listing;

namespace Web.ModelFactories.HomegameModelFactories
{
    public interface IHomegameListingItemModelFactory
    {
        HomegameListingItemModel Create(Homegame homegame);
    }
}