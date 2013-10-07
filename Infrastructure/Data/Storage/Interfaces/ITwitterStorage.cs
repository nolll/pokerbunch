using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	internal interface ITwitterStorage{

		RawTwitterCredentials GetCredentials(int userId);
		int AddCredentials(int userId, RawTwitterCredentials credentials);
		bool ClearCredentials(int userId);

	}

}