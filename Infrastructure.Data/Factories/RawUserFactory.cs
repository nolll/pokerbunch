using Core.Entities;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class RawUserFactory : IRawUserFactory
    {
        public RawUser Create(IStorageDataReader reader)
        {
            return new RawUser
            {
                Id = reader.GetIntValue("UserID"),
                UserName = reader.GetStringValue("UserName"),
                DisplayName = reader.GetStringValue("DisplayName"),
                RealName = reader.GetStringValue("RealName"),
                Email = reader.GetStringValue("Email"),
                GlobalRole = reader.GetIntValue("RoleID"),
                EncryptedPassword = reader.GetStringValue("Password"),
                Salt = reader.GetStringValue("Salt")
            };
        }

        public RawUser Create(User user)
        {
            return new RawUser
            {
                Id = user.Id,
                UserName = user.UserName,
                DisplayName = user.DisplayName,
                RealName = user.RealName,
                Email = user.Email,
                GlobalRole = (int)user.GlobalRole,
                EncryptedPassword = user.EncryptedPassword,
                Salt = user.Salt
            };
        }
    }
}
