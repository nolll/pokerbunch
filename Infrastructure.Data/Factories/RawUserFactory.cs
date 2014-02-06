﻿using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class RawUserFactory : IRawUserFactory
    {
        public RawUser Create(IStorageDataReader reader)
        {
            return new RawUser
            {
                Id = reader.GetInt("UserID"),
                UserName = reader.GetString("UserName"),
                DisplayName = reader.GetString("DisplayName"),
                RealName = reader.GetString("RealName"),
                Email = reader.GetString("Email"),
                GlobalRole = reader.GetInt("RoleID"),
                Token = reader.GetString("Token"),
                EncryptedPassword = reader.GetString("Password"),
                Salt = reader.GetString("Salt")
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
                Token = user.Token,
                EncryptedPassword = user.EncryptedPassword,
                Salt = user.Salt
            };
        }
    }
}