using Core.Classes;

namespace Core.Repositories {

	public interface ISharingRepository{

        IList<string> GetServices(User user);
		void AddSharing(User user, string sharingProvider);
		void RemoveSharing(User user, string sharingProvider);
		bool IsSharing(User user, string sharingProvider);

	}
}