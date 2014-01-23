using System.Collections.Generic;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface ISharingStorage{

		IList<string> GetServices(int userId);
        void AddSharing(int userId, string sharingProvider);
        void RemoveSharing(int userId, string sharingProvider);
        bool IsSharing(int userId, string sharingProvider);

	}

}