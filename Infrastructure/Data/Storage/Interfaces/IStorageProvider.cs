namespace Infrastructure.Data.Storage.Interfaces {

	public interface IStorageProvider {

		StorageDataReader Query(string sql);
        int Execute(string sql);
        int ExecuteInsert(string sql);
        int BoolToInt(bool boolean);

        /*
		function executePrepared($sql);

		function getLatestInsertId($success = true);

		function quote($string);
        */

	}

}