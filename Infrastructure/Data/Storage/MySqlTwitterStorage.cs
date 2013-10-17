using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Data.Storage {

	public class MySqlTwitterStorage : ITwitterStorage
    {
	    private readonly IStorageProvider _storageProvider;
	    private readonly IRawTwitterCredentialsFactory _rawTwitterCredentialsFactory;

	    public MySqlTwitterStorage(
            IStorageProvider storageProvider,
            IRawTwitterCredentialsFactory rawTwitterCredentialsFactory)
	    {
	        _storageProvider = storageProvider;
	        _rawTwitterCredentialsFactory = rawTwitterCredentialsFactory;
	    }

	    public RawTwitterCredentials GetCredentials(int userId){
			var sql = "SELECT ut.UserID, ut.TwitterName, ut.Key, ut.Secret FROM usertwitter ut WHERE ut.UserID = {0}";
	        sql = string.Format(sql, userId);
			return GetCredentialsFromSql(sql);
		}

		public int AddCredentials(int userId, RawTwitterCredentials credentials){
			var sql = "INSERT INTO usertwitter (UserID, TwitterName, `Key`, Secret) VALUES ({0}, '{1}', '{2}')";
			sql = string.Format(sql, userId, credentials.Key, credentials.Secret);
			return _storageProvider.ExecuteInsert(sql);
		}

		public bool ClearCredentials(int userId){
			var sql = "DELETE FROM usertwitter WHERE UserID = {0}";
			sql = string.Format(sql, userId);
            var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		private RawTwitterCredentials GetCredentialsFromSql(string sql){
			var reader = _storageProvider.Query(sql);
			if(reader.Read()){
				return _rawTwitterCredentialsFactory.Create(reader);
			}
			return null;
		}

	}

}