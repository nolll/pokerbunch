using MySql.Data.MySqlClient;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface IStorageProvider {

		MySqlDataReader Query(string sql);
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