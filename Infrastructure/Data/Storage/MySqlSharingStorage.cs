using System.Collections.Generic;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Data.Storage {

	public class MySqlSharingStorage : ISharingStorage
    {
	    private readonly IStorageProvider _storageProvider;

	    public MySqlSharingStorage(IStorageProvider storageProvider)
	    {
	        _storageProvider = storageProvider;
	    }

	    public IList<string> GetServices(int userId){
			var sql = "SELECT us.ServiceName FROM usersharing us WHERE us.UserID = {0}";
	        sql = string.Format(sql, userId);
			return GetServicesFromSql(sql);
		}

		public bool IsSharing(int userId, string sharingProvider){
			var sql = "SELECT us.UserID, us.ServiceName FROM usersharing us WHERE us.UserID = {0} AND us.ServiceName = '{1}'";
		    sql = string.Format(sql, userId, sharingProvider);
			return GetSharingStatusFromSql(sql);
		}

		public void AddSharing(int userId, string sharingProvider){
			var sql = "INSERT INTO usersharing (UserID, ServiceName) VALUES ({0}, '{1}')";
            sql = string.Format(sql, userId, sharingProvider);
			_storageProvider.ExecuteInsert(sql);
		}

		public void RemoveSharing(int userId, string sharingProvider){
			var sql = "DELETE FROM usersharing WHERE UserID = {0} AND ServiceName = '{1}'";
			sql = string.Format(sql, userId, sharingProvider);
            _storageProvider.Execute(sql);
		}

		private IList<string> GetServicesFromSql(string sql){
            var reader = _storageProvider.Query(sql);
		    var services = new List<string>();
			while(reader.Read()){
				services.Add(reader.GetString("ServiceName"));
			}
			return services;
		}

		private bool GetSharingStatusFromSql(string sql){
			var reader = _storageProvider.Query(sql);
            return reader.Read();
		}

    }

}