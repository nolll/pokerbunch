using Core.Classes;
using Web.Models.UserModels.Listing;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserItemModelFactory
    {
        UserItemModel Create(User user);
    }
}