using Core.Classes;
using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IAddUserPageModelFactory
    {
        AddUserPageModel Create(User user);
        AddUserPageModel Create(User user, AddUserPostModel postModel);
    }
}