using Application.UseCases.EditUserForm;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.SqlServer;

namespace Plumbing
{
    public class DependencyContainer
    {
        public EditUserFormInteractor EditUserForm { get; set; }

        public DependencyContainer()
        {
            var storageProvider = new SqlServerStorageProvider();
            var cacheProvider = new CacheProvider();
            var cacheContainer = new CacheContainer(cacheProvider);
            var cacheBuster = new CacheBuster(cacheContainer);
            
            var bunchStorage = new SqlServerBunchStorage(storageProvider);
            var bunchRepository = new BunchRepository(bunchStorage, cacheContainer, cacheBuster);

            var userStorage = new SqlServerUserStorage(storageProvider);
            var userRepository = new UserRepository(userStorage, cacheContainer, cacheBuster);

            EditUserForm = new EditUserFormInteractor(userRepository);
        }
    }
}