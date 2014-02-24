using System.Collections.Generic;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
    public class SqlServerUserStorage : IUserStorage 
    {
	    private readonly IStorageProvider _storageProvider;
	    private readonly IRawUserFactory _rawUserFactory;

        public SqlServerUserStorage(
            IStorageProvider storageProvider,
            IRawUserFactory rawUserFactory)
	    {
	        _storageProvider = storageProvider;
	        _rawUserFactory = rawUserFactory;
	    }

        public RawUser GetUserById(int id)
        {
            const string sql = "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Token, u.Password, u.Salt, u.RoleID FROM [User] u WHERE u.UserId = @userId";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@userId", id)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadOne(_rawUserFactory.Create);
        }

        public int? GetUserIdByNameOrEmail(string userNameOrEmail)
        {
            const string sql = "SELECT u.UserID FROM [User] u WHERE (u.UserName = @query OR u.Email = @query)";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@query", userNameOrEmail)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadInt("UserID");
        }

        public int? GetUserIdByToken(string token)
        {
            const string sql = "SELECT u.UserID FROM [User] u WHERE u.Token = @token";
            var parameters = new List<SimpleSqlParameter>
                {
                    new SimpleSqlParameter("@token", token)
                };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadInt("UserID");
        }

        public IList<RawUser> GetUserList(IList<int> ids)
        {
            const string sql = "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Token, u.Password, u.Salt, u.RoleID FROM [User] u WHERE u.UserID IN(@ids)";
            var parameter = new ListSqlParameter("@ids", ids);
            var reader = _storageProvider.Query(sql, parameter);
            return reader.ReadList(_rawUserFactory.Create);
        }

        public IList<int> GetUserIdList()
        {
            const string sql = "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Token, u.Password, u.Salt, u.RoleID FROM [User] u ORDER BY u.DisplayName";
            var reader = _storageProvider.Query(sql);
            return reader.ReadIntList("UserID");
        }

        public bool UpdateUser(RawUser user)
        {
            const string sql = "UPDATE [user] SET DisplayName = @displayName, RealName = @realName, Email = @email, Token = @token, Password = @password, Salt = @salt WHERE UserID = @userId";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@displayName", user.DisplayName),
		            new SimpleSqlParameter("@realName", user.RealName),
		            new SimpleSqlParameter("@email", user.Email),
		            new SimpleSqlParameter("@token", user.Token),
		            new SimpleSqlParameter("@password", user.EncryptedPassword),
		            new SimpleSqlParameter("@salt", user.Salt),
                    new SimpleSqlParameter("@userId", user.Id)
		        };
		    var rowCount = _storageProvider.Execute(sql, parameters);
			return rowCount > 0;
		}

		public int AddUser(RawUser user)
        {
            const string sql = "INSERT INTO [user] (UserName, DisplayName, Email, RoleId, Token, Password, Salt) VALUES (@userName, @displayName, @email, 1, @token, @password, @salt) SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@userName", user.UserName),
		            new SimpleSqlParameter("@displayName", user.DisplayName),
		            new SimpleSqlParameter("@email", user.Email),
		            new SimpleSqlParameter("@token", user.Token),
		            new SimpleSqlParameter("@password", user.EncryptedPassword),
		            new SimpleSqlParameter("@salt", user.Salt)
		        };
            return _storageProvider.ExecuteInsert(sql, parameters);
		}

		public bool DeleteUser(int userId)
        {
            const string sql = "DELETE FROM [user] WHERE UserID = @userId";
            var parameters = new List<SimpleSqlParameter>
		        {
		            new SimpleSqlParameter("@userId", userId)
		        };
			var rowCount = _storageProvider.Execute(sql, parameters);
			return rowCount > 0;
		}
	}
}