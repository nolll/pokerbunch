using Core.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface ITwitterStorage{

		TwitterCredentials GetCredentials(int userId);
		int AddCredentials(int userId, TwitterCredentials credentials);
		bool ClearCredentials(int userId);

	}

}