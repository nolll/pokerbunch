using Application.Factories;
using Core.Classes;
using Web.Models.UserModels.Add;
using Web.Models.UserModels.Edit;

namespace Web.ModelMappers
{
    public class UserModelMapper : IUserModelMapper
    {
        private readonly IUserFactory _userFactory;

        public UserModelMapper(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public User GetUser(AddUserPostModel postModel, string encryptedPassword, string salt)
        {
            return _userFactory.Create(
                0,
                postModel.UserName,
                postModel.DisplayName,
                string.Empty,
                postModel.Email,
                Role.Player,
                encryptedPassword,
                salt);
        }

        public User GetUser(User user, EditUserPostModel postModel)
        {
            return _userFactory.Create(
                user.Id,
                user.UserName,
                postModel.DisplayName,
                postModel.RealName,
                postModel.Email,
                user.GlobalRole,
                user.EncryptedPassword,
                user.Salt);
        }

        public User GetUser(User user, string encryptedPassword, string salt)
        {
            return _userFactory.Create(
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