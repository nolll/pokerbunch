using Core.Classes;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.Edit;

namespace Web.ModelMappers
{
    public class UserModelMapper : IUserModelMapper
    {
        public User GetUser(AddUserPostModel postModel)
        {
            return new User(
                0,
                postModel.UserName,
                postModel.DisplayName,
                string.Empty,
                postModel.Email,
                Role.Player);
        }

        public User GetUser(User user, EditUserPostModel postModel)
        {
            return new User(
                user.Id,
                user.UserName,
                postModel.DisplayName,
                postModel.RealName,
                postModel.Email,
                user.GlobalRole);
        }
    }
}