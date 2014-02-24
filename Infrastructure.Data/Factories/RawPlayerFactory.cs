using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class RawPlayerFactory : IRawPlayerFactory
    {
        public RawPlayer Create(IStorageDataReader reader)
        {
            return new RawPlayer
            {
                DisplayName = reader.GetStringValue("PlayerName"),
                Role = reader.GetIntValue("RoleID"),
                UserId = reader.GetIntValue("UserID"),
                Id = reader.GetIntValue("PlayerID")
            };
        }

    }
}
