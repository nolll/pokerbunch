namespace Infrastructure.Data.Storage.Interfaces {

	public interface IStorageProvider {

		StorageDataReader Query(string sql);
        int Execute(string sql);
        int ExecuteInsert(string sql);
        int BoolToInt(bool boolean);

	}

}