using Web.Models.UserModels.Add;

namespace Web.ModelFactories.UserModelFactories
{
    public interface IAddUserPageBuilder
    {
        AddUserPageModel Build(AddUserPostModel postModel);
    }
}