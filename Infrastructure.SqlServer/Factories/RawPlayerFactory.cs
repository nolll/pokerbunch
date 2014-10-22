using Infrastructure.SqlServer.Classes;
using Infrastructure.SqlServer.Interfaces;

namespace Infrastructure.SqlServer.Factories
{
    public static class RawPlayerFactory
    {
        public static RawPlayer Create(IStorageDataReader reader)
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
