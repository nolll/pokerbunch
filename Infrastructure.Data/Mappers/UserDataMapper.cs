using Application.Factories;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public class UserDataMapper : IUserDataMapper
    {
        private readonly IUserFactory _userFactory;

        public UserDataMapper(IUserFactory userFactory)
        {
            _userFactory = userFactory;
        }

        public User Map(RawUser rawUser)
        {
            return _userFactory.Create(
                rawUser.Id,
                rawUser.UserName,
                rawUser.DisplayName,
                rawUser.RealName,
                rawUser.Email,
                (Role)rawUser.GlobalRole,
                rawUser.EncryptedPassword,
                rawUser.Salt);
        }
    }
}