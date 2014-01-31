namespace Infrastructure.Data.Storage {

	public interface IStorageProvider {

		IStorageDataReader Query(string sql);
        int Execute(string sql);
        int ExecuteInsert(string sql);
        int BoolToInt(bool boolean);

	}

}