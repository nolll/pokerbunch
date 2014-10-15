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
    }
}