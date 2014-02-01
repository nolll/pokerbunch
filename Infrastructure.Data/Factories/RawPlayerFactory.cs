﻿using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class RawPlayerFactory : IRawPlayerFactory
    {
        public RawPlayer Create(IStorageDataReader reader)
        {
            return new RawPlayer
            {
                DisplayName = reader.GetString("PlayerName"),
                Role = reader.GetInt("RoleID"),
                UserId = reader.GetInt("UserID"),
                Id = reader.GetInt("PlayerID")
            };
        }

    }
}
