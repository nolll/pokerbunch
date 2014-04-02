using Core.Classes;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.Edit;

namespace Web.ModelMappers
{
    public interface IUserModelMapper
    {
        User GetUser(AddUserPostModel postModel, string encryptedPassword, string salt);
        User GetUser(User user, EditUserPostModel postModel);
        User GetUser(User user, string encryptedPassword, string salt);
    }
}