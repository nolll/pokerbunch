using Core.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface ITwitterStorage{

		TwitterCredentials GetCredentials(User user);
		int AddCredentials(User user, TwitterCredentials credentials);
		bool ClearCredentials(User user);

	}

}