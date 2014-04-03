using Core.Classes;
using Core.UseCases;
using Web.Models.UserModels.List;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IUserListPageModelFactory
    {
        UserListPageModel Create(User user, ShowUserListResult showUserListResult);
    }
}