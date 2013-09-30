using Core.Classes;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.Edit;

namespace Web.ModelMappers
{
    public class UserModelMapper : IUserModelMapper
    {
        public User GetUser(AddUserPostModel postModel)
        {
            return new User
            {
                UserName = postModel.UserName,
                DisplayName = postModel.DisplayName,
                RealName = string.Empty,
                Email = postModel.Email,
                GlobalRole = Role.Player,
                Id = 0
            };
        }

        public User GetUser(User user, EditUserPostModel postModel)
        {
            return new User
            {
                UserName = user.UserName,
                DisplayName = postModel.DisplayName,
                RealName = postModel.RealName,
                Email = postModel.Email,
                GlobalRole = user.GlobalRole,
                Id = user.Id,
            };
        }
    }
}