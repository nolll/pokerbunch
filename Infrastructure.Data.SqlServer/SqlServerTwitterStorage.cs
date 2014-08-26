using System.Collections.Generic;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
	public class SqlServerTwitterStorage : ITwitterStorage
    {
	    private readonly IStorageProvider _storageProvider;

        public SqlServerTwitterStorage(
            IStorageProvider storageProvider)
	    {
	        _storageProvider = storageProvider;
	    }

	    public RawTwitterCredentials GetCredentials(int userId)
        {
			const string sql = "SELECT ut.UserId, ut.TwitterName, ut.[Key], ut.Secret FROM usertwitter ut WHERE ut.UserId = @userId";
            var parameters = new List<SimpleSqlParameter>
	            {
	                new SimpleSqlParameter("@userId", userId)
	            };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadOne(RawTwitterCredentialsFactory.Create);
		}

		public int AddCredentials(int userId, RawTwitterCredentials credentials)
        {
            const string sql = "INSERT INTO usertwitter (UserId, TwitterName, [Key], Secret) VALUES (@userId, @twitterName, @key, @secret)";
            var parameters = new List<SimpleSqlParameter>
	            {
	                new SimpleSqlParameter("@userId", userId),
                    new SimpleSqlParameter("@twitterName", credentials.TwitterName),
                    new SimpleSqlParameter("@key", credentials.Key),
                    new SimpleSqlParameter("@secret", credentials.Secret)
	            };
			return _storageProvider.ExecuteInsert(sql, parameters);
		}

		public bool ClearCredentials(int userId)
        {
			const string sql = "DELETE FROM usertwitter WHERE UserId = @userId";
            var parameters = new List<SimpleSqlParameter>
	            {
	                new SimpleSqlParameter("@userId", userId)
	            };
            var rowCount = _storageProvider.Execute(sql, parameters);
			return rowCount > 0;
		}
	}
}