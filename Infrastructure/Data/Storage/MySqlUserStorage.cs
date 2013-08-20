using System.Collections.Generic;
using System.Data;
using Core.Classes;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Factories;
using MySql.Data.MySqlClient;

namespace Infrastructure.Data.Storage {
    public class MySqlUserStorage : IUserStorage 
    {
	    private readonly IStorageProvider _storageProvider;
	    private readonly IUserFactory _userFactory;

	    public MySqlUserStorage(IStorageProvider storageProvider, IUserFactory userFactory)
	    {
	        _storageProvider = storageProvider;
	        _userFactory = userFactory;
	    }

		public User GetUserByEmail(string email){
			var sql = GetUserBaseSql();
			sql += "WHERE u.Email = '{0}'";
		    sql = string.Format(sql, email);
			return getUser(sql);
		}

		public User GetUserByName(string userName){
			var sql = GetUserBaseSql();
			sql += "WHERE u.UserName = '{0}'";
            sql = string.Format(sql, userName);
			return getUser(sql);
		}

		public User GetUserByToken(string token){
			var sql =	GetUserBaseSql();
			sql +=	"WHERE u.Token = '{0}'";
			sql = string.Format(sql, token);
            return getUser(sql);
		}

		public User GetUserByCredentials(string userNameOrEmail, string password){
			var sql = GetUserBaseSql();
			sql += "WHERE (u.UserName = '{0}' OR u.Email = '{0}') AND u.Password = '{1}'";
			sql = string.Format(sql, userNameOrEmail, password);
            return getUser(sql);
		}

		private string GetUserBaseSql(){
			return "SELECT u.UserID, u.UserName, u.DisplayName, u.RealName, u.Email, u.Token, u.Password, u.Salt, u.RoleID FROM user u ";
		}

		private User getUser(string sql){
            var reader = _storageProvider.Query(sql);
            while (reader.Read())
            {
                return UserFromDbRow(reader);
            }
			return null;
		}

		public List<User> GetUsers(){
			var sql =	GetUserBaseSql();
			sql +=	"ORDER BY u.DisplayName";
			var reader = _storageProvider.Query(sql);
			var users = new List<User>();
            while (reader.Read())
            {
                users.Add(UserFromDbRow(reader));
            }
			return users;
		}

		public bool UpdateUser(User user){
			var sql =	"UPDATE user u " +
					"SET " +
						"DisplayName = '{0}', " +
						"RealName = '{1}', " +
						"Email = '{2}' " +
					"WHERE UserID = {3}";
		    sql = string.Format(sql, user.DisplayName, user.RealName, user.Email, user.Id);
		    var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public int AddUser(User user){
			var sql =	"INSERT INTO user " +
					"(UserName, DisplayName, Email) " +
					"VALUES " +
					"('{0}', '{1}', '{2}')";
            sql = string.Format(sql, user.UserName, user.DisplayName, user.Email);
            var id = _storageProvider.ExecuteInsert(sql);
			return id;
		}

		public bool DeleteUser(User user){
			var sql =	"DELETE FROM user u " +
					"WHERE UserID = {0}";
            sql = string.Format(sql, user.Id);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public string GetSalt(string userNameOrEmail){
			var sql =	"SELECT u.Salt " +
					"FROM user u " +
					"WHERE (u.UserName = '{0}' OR u.Email = '{0}')";
            sql = string.Format(sql, userNameOrEmail);
            var reader = _storageProvider.Query(sql);
			while(reader.Read()){
				return reader.GetString("Salt");
			}
			return "";
		}

		public bool SetSalt(User user, string salt){
			var sql =	"UPDATE user u " +
				"SET " +
					"Salt = '{0}' " +
				"WHERE UserName = '{1}'";
		    sql = string.Format(sql, salt, user.UserName);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public bool SetEncryptedPassword(User user, string encryptedPassword){
			var sql =	"UPDATE user u " +
				"SET " +
					"Password = '{0}' " +
				"WHERE UserName = '{1}'";
			sql = string.Format(sql, encryptedPassword, user.UserName);
            var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public bool SetToken(User user, string token){
			var sql =	"UPDATE user u " +
				"SET " +
					"Token = '{0}' " +
				"WHERE UserName = '{1}'";
            sql = string.Format(sql, token, user.UserName);
			var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		public string GetToken(User user){
			var sql =	"SELECT u.Token " +
				"FROM user u " +
				"WHERE u.UserName = '{0}'";
            sql = string.Format(sql, user.UserName);
			var reader = _storageProvider.Query(sql);
			while(reader.Read())
			{
			    return reader.GetString("Token");
			}
			return null;
		}

        private User UserFromDbRow(StorageDataReader reader){
			var id = reader.GetInt("UserID");
            var userName = reader.GetString("UserName");
			var displayName = reader.GetString("DisplayName");
			var realName = reader.GetString("RealName");
			var email = reader.GetString("Email");
			var globalRole = (Role)reader.GetInt("RoleID");
			return _userFactory.Create(id, userName, displayName, realName, email, globalRole);
		}

	}

}