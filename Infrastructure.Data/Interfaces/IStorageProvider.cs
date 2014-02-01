namespace Infrastructure.Data.Interfaces {

	public interface IStorageProvider {

		IStorageDataReader Query(string sql);
        int Execute(string sql);
        int ExecuteInsert(string sql);
        int BoolToInt(bool boolean);

	}

}