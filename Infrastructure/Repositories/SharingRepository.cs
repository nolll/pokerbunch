using System.Collections.Generic;
using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Storage.Interfaces;

namespace Infrastructure.Repositories
{
    public class SharingRepository : ISharingRepository
    {
        private readonly ISharingStorage _sharingStorage;

        public SharingRepository(ISharingStorage sharingStorage)
        {
            _sharingStorage = sharingStorage;
        }

        public IList<string> GetServices(User user)
        {
            return _sharingStorage.GetServices(user);
        }

        public void AddSharing(User user, string sharingProvider)
        {
            _sharingStorage.AddSharing(user, sharingProvider);
        }

        public void RemoveSharing(User user, string sharingProvider)
        {
            _sharingStorage.RemoveSharing(user, sharingProvider);
        }

        public bool IsSharing(User user, string sharingProvider)
        {
            return _sharingStorage.IsSharing(user, sharingProvider);
        }
    }
}