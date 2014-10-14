using Core.Entities;
using Web.Models.UserModels.Edit;

namespace Web.ModelMappers
{
    public static class UserModelMapper
    {
        public static User GetUser(User user, EditUserPostModel postModel)
        {
            return new User(
                user.Id,
                user.UserName,
                postModel.DisplayName,
                postModel.RealName,
                postModel.Email,
                user.GlobalRole,
                user.EncryptedPassword,
                user.Salt);
        }

        public static User GetUser(User user, string encryptedPassword, string salt)
        {
            return new User(
                user.Id,
                user.UserName,
                user.DisplayName,
                user.RealName,
                user.Email,
                user.GlobalRole,
                encryptedPassword,
                salt);
        }
    }
}