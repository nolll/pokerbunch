using Core.Classes;
using Web.Models.UserModels.Edit;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IEditUserPageModelFactory
    {
        EditUserPageModel Create(User user);
        EditUserPageModel Create(User user, EditUserPostModel postModel);
    }
}