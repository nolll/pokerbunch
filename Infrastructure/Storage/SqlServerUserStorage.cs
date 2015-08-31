using System.Collections.Generic;
using Infrastructure.Storage.Classes;
using Infrastructure.Storage.Interfaces;

namespace Infrastructure.Storage
{
    public class SqlServerUserStorage : SqlServerStorageProvider 
    {
        private const string UserDataSql = "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Password, u.Salt, u.RoleID FROM [User] u ";
        private const string UserIdSql = "SELECT u.UserID FROM [User] u ";

        public RawUser GetUserById(int id)
        {
            var sql = string.Concat(UserDataSql, "WHERE u.UserId = @userId");
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@userId", id)
                };
            var reader = Query(sql, parameters);
            return reader.ReadOne(CreateRawUser);
        }

        public IList<RawUser> GetUserList(IList<int> ids)
        {
            var sql = string.Concat(UserDataSql, "WHERE u.UserID IN(@ids)");
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = Query(sql, parameter);
            return reader.ReadList(CreateRawUser);
        }

        public int? GetUserIdByNameOrEmail(string userNameOrEmail)
        {
            var sql = string.Concat(UserIdSql, "WHERE (u.UserName = @query OR u.Email = @query)");
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@query", userNameOrEmail)
                };
            var reader = Query(sql, parameters);
            return reader.ReadInt("UserID");
        }

        public IList<int> GetUserIdList()
        {
            var sql = string.Concat(UserIdSql, "ORDER BY u.DisplayName");
            var reader = Query(sql);
            return reader.ReadIntList("UserID");
        }

        public bool UpdateUser(RawUser user)
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
		    var rowCount = Execute(sql, parameters);
			return rowCount > 0;
        }

		public int AddUser(RawUser user)
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
            return ExecuteInsert(sql, parameters);
		}

		public bool DeleteUser(int userId)
        {
            const string sql = "DELETE FROM [user] WHERE UserID = @userId";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@userId", userId)
		        };
			var rowCount = Execute(sql, parameters);
			return rowCount > 0;
		}

        private static RawUser CreateRawUser(IStorageDataReader reader)
        {
            return new RawUser(
                reader.GetIntValue("UserID"),
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