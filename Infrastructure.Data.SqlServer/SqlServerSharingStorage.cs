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
            var reader = _storageProvider.Query(sql, parameters);
            return reader.ReadStringList("ServiceName");
		}

		public bool IsSharing(int userId, string sharingProvider)
        {
			const string sql = "SELECT us.UserID, us.ServiceName FROM usersharing us WHERE us.UserID = @userId AND us.ServiceName = @serviceName";
            var parameters = new List<SimpleSqlParameter>
	            {
	                new SimpleSqlParameter("@userId", userId),
                    new SimpleSqlParameter("@serviceName", sharingProvider)
	            };
            var reader = _storageProvider.Query(sql, parameters);
            return reader.HasRows();
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
    }
}