using Application.Factories;
using Core.Entities;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.Edit;

namespace Web.ModelMappers
{
    public static class UserModelMapper
    {
        public static User GetUser(AddUserPostModel postModel, string encryptedPassword, string salt)
        {
            return UserFactory.Create(
                0,
                postModel.UserName,
                postModel.DisplayName,
                string.Empty,
                postModel.Email,
                Role.Player,
                encryptedPassword,
                salt);
        }

        public static User GetUser(User user, EditUserPostModel postModel)
        {
            return UserFactory.Create(
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
            return UserFactory.Create(
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