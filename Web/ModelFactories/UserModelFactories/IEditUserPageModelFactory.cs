using Core.Entities;
using Web.Models.UserModels.Edit;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IEditUserPageModelFactory
    {
        EditUserPageModel Create(User user, EditUserPostModel postModel);
    }
}