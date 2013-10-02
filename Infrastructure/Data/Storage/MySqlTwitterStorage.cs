using Core.Classes;
using Infrastructure.Data.Storage.Interfaces;
using Infrastructure.Integration.Twitter;

namespace Infrastructure.Data.Storage {

	public class MySqlTwitterStorage : ITwitterStorage
    {
	    private readonly IStorageProvider _storageProvider;

	    public MySqlTwitterStorage(IStorageProvider storageProvider)
	    {
	        _storageProvider = storageProvider;
	    }

	    public TwitterCredentials GetCredentials(User user){
			var sql = "SELECT ut.UserID, ut.TwitterName, ut.Key, ut.Secret FROM usertwitter ut WHERE ut.UserID = {0}";
	        sql = string.Format(sql, user.Id);
			return GetCredentialsFromSql(sql);
		}

		public int AddCredentials(User user, TwitterCredentials credentials){
			var sql = "INSERT INTO usertwitter (UserID, TwitterName, `Key`, Secret) VALUES ({0}, '{1}', '{2}')";
			sql = string.Format(sql, user.Id, credentials.Key, credentials.Secret);
			return _storageProvider.ExecuteInsert(sql);
		}

		public bool ClearCredentials(User user){
			var sql = "DELETE FROM usertwitter WHERE UserID = {0}";
			sql = string.Format(sql, user.Id);
            var rowCount = _storageProvider.Execute(sql);
			return rowCount > 0;
		}

		private TwitterCredentials GetCredentialsFromSql(string sql){
			var reader = _storageProvider.Query(sql);
			if(reader.Read()){
				return CredentialsFromDbRow(reader);
			}
			return null;
		}

		private TwitterCredentials CredentialsFromDbRow(StorageDataReader reader){
			return new TwitterCredentials
			    {
			        TwitterName = reader.GetString("TwitterName"),
			        Key = reader.GetString("Key"),
			        Secret = reader.GetString("Secret")
			    };
		}

	}

}