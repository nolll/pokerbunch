﻿using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage.Repositories
{
    public class SqlUserRepository : IUserRepository
    {
        private const string DataSql = "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Password, u.Salt, u.RoleID FROM [User] u ";
        private const string SearchSql = "SELECT u.UserID FROM [User] u ";
        
        private readonly SqlServerStorageProvider _db;

        public SqlUserRepository(SqlServerStorageProvider db)
        {
            _db = db;
        }

        public User Get(string id)
        {
            var sql = string.Concat(DataSql, "WHERE u.UserId = @userId");
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@userId", id)
            };
            var reader = _db.Query(sql, parameters);
            var rawUser = reader.ReadOne(CreateRawUser);
            return rawUser != null ? RawUser.CreateReal(rawUser) : null;
        }

        public IList<User> Get(IList<string> ids)
        {
            var sql = string.Concat(DataSql, "WHERE u.UserID IN(@ids)");
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = _db.Query(sql, parameter);
            var rawUsers = reader.ReadList(CreateRawUser);
            return rawUsers.Select(RawUser.CreateReal).OrderBy(o => o.DisplayName).ToList();
        }

        public IList<string> Find()
        {
            return GetIds();
        }

        public IList<string> Find(string nameOrEmail)
        {
            var userId = GetIdByNameOrEmail(nameOrEmail);
            if(!string.IsNullOrEmpty(userId))
                return new List<string>{userId};
            return new List<string>();
        }

        public void Update(User user)
        {
            const string sql = "UPDATE [user] SET DisplayName = @displayName, RealName = @realName, Email = @email, Password = @password, Salt = @salt WHERE UserID = @userId";
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@displayName", user.DisplayName),
                new SimpleSqlParameter("@realName", user.RealName),
                new SimpleSqlParameter("@email", user.Email),
                new SimpleSqlParameter("@password", user.EncryptedPassword),
                new SimpleSqlParameter("@salt", user.Salt),
                new SimpleSqlParameter("@userId", user.Id)
            };
            _db.Execute(sql, parameters);
        }

        public string Add(User user)
        {
            const string sql = "INSERT INTO [user] (UserName, DisplayName, Email, RoleId, Password, Salt) VALUES (@userName, @displayName, @email, 1, @password, @salt) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@userName", user.UserName),
                new SimpleSqlParameter("@displayName", user.DisplayName),
                new SimpleSqlParameter("@email", user.Email),
                new SimpleSqlParameter("@password", user.EncryptedPassword),
                new SimpleSqlParameter("@salt", user.Salt)
            };
            return _db.ExecuteInsert(sql, parameters);
        }
        
        private string GetIdByNameOrEmail(string nameOrEmail)
        {
            var sql = string.Concat(SearchSql, "WHERE (u.UserName = @query OR u.Email = @query)");
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@query", nameOrEmail)
            };
            var reader = _db.Query(sql, parameters);
            return reader.ReadInt("UserID").ToString();
        }

        private IList<string> GetIds()
        {
            var sql = string.Concat(SearchSql, "ORDER BY u.DisplayName");
            var reader = _db.Query(sql);
            return reader.ReadIntList("UserID").Select(o => o.ToString()).ToList();
        }

        public bool DeleteUser(int userId)
        {
            const string sql = "DELETE FROM [user] WHERE UserID = @userId";
            var parameters = new List<SimpleSqlParameter>
            {
                new SimpleSqlParameter("@userId", userId)
            };
            var rowCount = _db.Execute(sql, parameters);
            return rowCount > 0;
        }

        private static RawUser CreateRawUser(IStorageDataReader reader)
        {
            return new RawUser(
                reader.GetIntValue("UserID").ToString(),
                reader.GetStringValue("UserName"),
                reader.GetStringValue("DisplayName"),
                reader.GetStringValue("RealName"),
                reader.GetStringValue("Email"),
                reader.GetIntValue("RoleID"),
                reader.GetStringValue("Password"),
                reader.GetStringValue("Salt"));
        }
    }
}
