using Core.Classes;
using Infrastructure.Integration.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface ITwitterStorage{

		TwitterCredentials GetCredentials(User user);
		bool AddCredentials(User user, TwitterCredentials credentials);
		bool ClearCredentials(User user);

	}

}