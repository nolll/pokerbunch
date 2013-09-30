using Core.Classes;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.Edit;

namespace Web.ModelMappers
{
    public interface IUserModelMapper
    {
        User GetUser(AddUserPostModel postModel);
        User GetUser(User user, EditUserPostModel postModel);
    }
}