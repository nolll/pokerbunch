using Core.Entities;

namespace Core.Repositories {

	public interface ITwitterRepository{

		TwitterCredentials GetCredentials(User user);
		int AddCredentials(User user, TwitterCredentials credentials);

	}
}