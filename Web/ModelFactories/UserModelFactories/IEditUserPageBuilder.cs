using Web.Models.UserModels.Edit;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IEditUserPageBuilder
    {
        EditUserPageModel Build(string userName, EditUserPostModel postModel = null);
    }
}