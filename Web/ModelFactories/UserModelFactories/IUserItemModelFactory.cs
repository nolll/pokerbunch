using Core.UseCases;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserItemModelFactory
    {
        UserItemModel Create(UserItem userItem);
    }
}