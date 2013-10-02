using System.Collections.Generic;
using Core.Classes;

namespace Infrastructure.Data.Storage.Interfaces {

	public interface ISharingStorage{

		IList<string> GetServices(User user);
		void AddSharing(User user, string sharingProvider);
		void RemoveSharing(User user, string sharingProvider);
		bool IsSharing(User user, string sharingProvider);

	}

}