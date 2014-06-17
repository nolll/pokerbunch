using Core.Entities;
using Web.Models.UserModels.Edit;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IEditUserPageBuilder
    {
        EditUserPageModel Build(User user, EditUserPostModel postModel);
    }
}