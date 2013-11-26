using Core.Classes;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.Edit;

namespace Web.ModelMappers
{
    public class UserModelMapper : IUserModelMapper
    {
        public User GetUser(AddUserPostModel postModel, string token, string encryptedPassword, string salt)
        {
            return new User(
                0,
                postModel.UserName,
                postModel.DisplayName,
                string.Empty,
                postModel.Email,
                Role.Player,
                token,
                encryptedPassword,
                salt);
        }

        public User GetUser(User user, EditUserPostModel postModel)
        {
            return new User(
                user.Id,
                user.UserName,
                postModel.DisplayName,
                postModel.RealName,
                postModel.Email,
                user.GlobalRole,
                user.Token,
                user.EncryptedPassword,
                user.Salt);
        }

        public User GetUser(User user, string encryptedPassword, string salt)
        {
            return new User(
                user.Id,
                user.UserName,
                user.DisplayName,
                user.RealName,
                user.Email,
                user.GlobalRole,
                user.Token,
                encryptedPassword,
                salt);
        }
    }
}