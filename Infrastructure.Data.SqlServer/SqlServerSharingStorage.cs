using System.Collections.Generic;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.SqlServer
{
	public class SqlServerSharingStorage : ISharingStorage
    {
	    private readonly IStorageProvider _storageProvider;

        public SqlServerSharingStorage(
            IStorageProvider storageProvider)
	    {
	        _storageProvider = storageProvider;
	    }

	    public IList<string> GetServices(int userId)
        {
			const string sql = "SELECT us.ServiceName FROM usersharing us WHERE us.UserID = @userId";
            var parameters = new List<SimpleSqlParameter>
	            {
	                new SimpleSqlParameter("@userId", userId)
	            };
			return GetServiceList(sql, parameters);
		}

		public bool IsSharing(int userId, string sharingProvider)
        {
			const string sql = "SELECT us.UserID, us.ServiceName FROM usersharing us WHERE us.UserID = @userId AND us.ServiceName = @serviceName";
            var parameters = new List<SimpleSqlParameter>
	            {
	                new SimpleSqlParameter("@userId", userId),
                    new SimpleSqlParameter("@serviceName", sharingProvider)
	            };
            return GetSharingStatus(sql, parameters);
		}

		public void AddSharing(int userId, string sharingProvider)
        {
            const string sql = "INSERT INTO usersharing (UserID, ServiceName) OUTPUT INSERTED.UserId VALUES (@userId, @serviceName)";
            var parameters = new List<SimpleSqlParameter>
	            {
	                new SimpleSqlParameter("@userId", userId),
                    new SimpleSqlParameter("@serviceName", sharingProvider)
	            };
            _storageProvider.ExecuteInsert(sql, parameters);
		}

		public void RemoveSharing(int userId, string sharingProvider)
        {
			const string sql = "DELETE FROM usersharing WHERE UserID = @userId AND ServiceName = @serviceName";
            var parameters = new List<SimpleSqlParameter>
	            {
	                new SimpleSqlParameter("@userId", userId),
                    new SimpleSqlParameter("@serviceName", sharingProvider)
	            };
            _storageProvider.Execute(sql, parameters);
		}

        private IList<string> GetServiceList(string sql, IList<SimpleSqlParameter> parameters)
        {
            var reader = _storageProvider.Query(sql, parameters);
		    var services = new List<string>();
			while(reader.Read()){
				services.Add(reader.GetStringValue("ServiceName"));
			}
			return services;
		}

        private bool GetSharingStatus(string sql, IList<SimpleSqlParameter> parameters)
        {
			var reader = _storageProvider.Query(sql, parameters);
            return reader.Read();
		}
    }
}