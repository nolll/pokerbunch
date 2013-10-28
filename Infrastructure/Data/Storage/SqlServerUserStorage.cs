using System.Collections.Generic;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;

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

		public RawUser GetUserByEmail(string email){
			var sql = GetUserBaseSql();
			sql += "WHERE u.Email = '{0}'";
		    sql = string.Format(sql, email);
			return GetUser(sql);
		}

		public RawUser GetUserByName(string userName){
			var sql = GetUserBaseSql();
			sql += "WHERE u.UserName = '{0}'";
            sql = string.Format(sql, userName);
			return GetUser(sql);
		}

		public RawUser GetUserByToken(string token){
			var sql =	GetUserBaseSql();
			sql +=	"WHERE u.Token = '{0}'";
			sql = string.Format(sql, token);
            return GetUser(sql);
		}

		public RawUser GetUserByCredentials(string userNameOrEmail, string password){
			var sql = GetUserBaseSql();
			sql += "WHERE (u.UserName = '{0}' OR u.Email = '{0}') AND u.Password = '{1}'";
			sql = string.Format(sql, userNameOrEmail, password);
            return GetUser(sql);
		}

		private string GetUserBaseSql(){
			return "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Token, u.Password, u.Salt, u.RoleID FROM [User] u ";
		}

		private RawUser GetUser(string sql){
            var reader = _storageProvider.Query(sql);
            while (reader.Read())
            {
                return _rawUserFactory.Create(reader);
            }
			return null;
		}

		public List<RawUser> GetUsers(){
			var sql =	GetUserBaseSql();
			sql +=	"ORDER BY u.DisplayName";
			var reader = _storageProvider.Query(sql);
			var users = new List<RawUser>();
            while (reader.Read())
            {
                users.Add(_rawUserFactory.Create(reader));
            }
			return users;
		}

		public bool UpdateUser(RawUser user){
            var sql = "UPDATE [user] SET DisplayName = '{0}', RealName = '{1}', Email = '{2}' WHERE UserID = {3}";
		    sql = string.Format(sql, user.DisplayName, user.RealName, user.Email, user.Id);
		    var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public int AddUser(RawUser user){
            var sql = "INSERT INTO [user] (UserName, DisplayName, Email) VALUES ('{0}', '{1}', '{2}') SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY]";
            sql = string.Format(sql, user.UserName, user.DisplayName, user.Email);
            var id = _storageProvider.ExecuteInsert(sql);
			return id;
		}

		public bool DeleteUser(int userId){
            var sql = "DELETE FROM [user] WHERE UserID = {0}";
            sql = string.Format(sql, userId);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public string GetSalt(string userNameOrEmail){
            var sql = "SELECT u.Salt FROM [user] u WHERE (u.UserName = '{0}' OR u.Email = '{0}')";
            sql = string.Format(sql, userNameOrEmail);
            var reader = _storageProvider.Query(sql);
			while(reader.Read()){
				return reader.GetString("Salt");
			}
			return "";
		}

		public bool SetSalt(string userName, string salt){
            var sql = "UPDATE [user] SET Salt = '{0}' WHERE UserName = '{1}'";
		    sql = string.Format(sql, salt, userName);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public bool SetEncryptedPassword(string userName, string encryptedPassword){
            var sql = "UPDATE [user] SET Password = '{0}' WHERE UserName = '{1}'";
			sql = string.Format(sql, encryptedPassword, userName);
            var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public bool SetToken(string userName, string token){
            var sql = "UPDATE [user] SET Token = '{0}' WHERE UserName = '{1}'";
            sql = string.Format(sql, token, userName);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public string GetToken(string userName){
            var sql = "SELECT u.Token FROM [user] u WHERE u.UserName = '{0}'";
            sql = string.Format(sql, userName);
			var reader = _storageProvider.Query(sql);
			while(reader.Read())
			{
			    return reader.GetString("Token");
			}
			return null;
		}

	}

}