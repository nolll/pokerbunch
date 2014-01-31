using System.Collections.Generic;
using System.Linq;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;

namespace Infrastructure.Data.Storage {
    public class SqlServerUserStorage : IUserStorage 
    {
	    private readonly IStorageProvider _storageProvider;
	    private readonly IRawUserFactory _rawUserFactory;

        public SqlServerUserStorage(IStorageProvider storageProvider, IRawUserFactory rawUserFactory)
	    {
	        _storageProvider = storageProvider;
	        _rawUserFactory = rawUserFactory;
	    }

        public RawUser GetUserById(int id)
        {
            var sql = GetUserBaseSql();
            sql += "WHERE u.UserId = {0}";
            sql = string.Format(sql, id);
            return GetUser(sql);
        }

        public int? GetUserIdByNameOrEmail(string userNameOrEmail)
        {
            var sql = GetUserIdBaseSql();
            sql += "WHERE (u.UserName = '{0}' OR u.Email = '{0}')";
            sql = string.Format(sql, userNameOrEmail);
            return GetUserId(sql);
        }

        public int? GetUserIdByToken(string token)
        {
            var sql = GetUserIdBaseSql();
            sql += "WHERE u.Token = '{0}'";
            sql = string.Format(sql, token);
            return GetUserId(sql);
        }

        private string GetUserBaseSql()
        {
            return "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Token, u.Password, u.Salt, u.RoleID FROM [User] u ";
        }

        private string GetUserIdBaseSql()
        {
            return "SELECT u.UserID FROM [User] u ";
        }

        private RawUser GetUser(string sql)
        {
            var reader = _storageProvider.Query(sql);
            while (reader.Read())
            {
                return _rawUserFactory.Create(reader);
            }
            return null;
        }

        private int? GetUserId(string sql)
        {
            var reader = _storageProvider.Query(sql);
            while (reader.Read())
            {
                return reader.GetInt("UserID");
            }
            return null;
        }

        public IList<RawUser> GetUsers(IEnumerable<int> ids)
        {
            var baseStatement = GetUserBaseSql();
            const string statement = "{0} WHERE u.UserID IN({1})";
            var idList = GetIdListForSql(ids);
            var sql = string.Format(statement, baseStatement, idList);
            var reader = _storageProvider.Query(sql);
            var users = new List<RawUser>();
            while (reader.Read())
            {
                users.Add(_rawUserFactory.Create(reader));
            }
            return users;
        }

        private string GetIdListForSql(IEnumerable<int> ids)
        {
            return string.Join(", ", ids.Select(o => string.Format("{0}", o)).ToArray());
        }

        public IList<int> GetUserIds()
        {
            var sql = GetUserBaseSql();
            sql += "ORDER BY u.DisplayName";
            var reader = _storageProvider.Query(sql);
            var ids = new List<int>();
            while (reader.Read())
            {
                ids.Add(reader.GetInt("UserID"));
            }
            return ids;
        }

		public bool UpdateUser(RawUser user){
            var sql = "UPDATE [user] SET DisplayName = '{0}', RealName = '{1}', Email = '{2}', Token = '{3}', Password = '{4}', Salt = '{5}' WHERE UserID = {6}";
		    sql = string.Format(sql, user.DisplayName, user.RealName, user.Email, user.Token, user.EncryptedPassword, user.Salt, user.Id);
		    var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public int AddUser(RawUser user){
            var sql = "INSERT INTO [user] (UserName, DisplayName, Email, RoleId, Token, Password, Salt) VALUES ('{0}', '{1}', '{2}', 1, '{3}', '{4}', '{5}') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            sql = string.Format(sql, user.UserName, user.DisplayName, user.Email, user.Token, user.EncryptedPassword, user.Salt);
            var id = _storageProvider.ExecuteInsert(sql);
			return id;
		}

		public bool DeleteUser(int userId){
            var sql = "DELETE FROM [user] WHERE UserID = {0}";
            sql = string.Format(sql, userId);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

	}

}