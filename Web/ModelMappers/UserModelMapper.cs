using Core.Classes;
using Web.Models.UserModels.Add;

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
                Email = postModel.Email,
                GlobalRole = Role.Player,
                Id = 0,
                RealName = string.Empty
            };
        }
    }
}