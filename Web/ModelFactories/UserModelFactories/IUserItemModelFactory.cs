using Core.Classes;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserItemModelFactory
    {
        UserItemModel Create(User user);
    }
}