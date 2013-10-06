using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    public class RawPlayerFactory : IRawPlayerFactory
    {
        public RawPlayer Create(StorageDataReader reader)
        {
            return new RawPlayer
            {
                DisplayName = reader.GetString("DisplayName"),
                Role = reader.GetInt("RoleID"),
                UserName = reader.GetString("UserName"),
                Id = reader.GetInt("PlayerID")
            };
        }

    }
}
