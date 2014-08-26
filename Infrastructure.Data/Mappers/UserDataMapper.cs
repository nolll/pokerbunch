using Application.Factories;
using Core.Entities;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public static class UserDataMapper
    {
        public static User Map(RawUser rawUser)
        {
            return UserFactory.Create(
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