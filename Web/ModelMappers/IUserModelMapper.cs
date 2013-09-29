using Core.Classes;
using Web.Models.UserModels.Add;

namespace Web.ModelMappers
{
    public interface IUserModelMapper
    {
        User GetUser(AddUserPostModel postModel);
        //Homegame GetUser(User user, UserEditPostModel postModel);
    }
}