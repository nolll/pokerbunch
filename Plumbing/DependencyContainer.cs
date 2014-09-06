using System;
using Application.UseCases.EditUserForm;
using Application.UseCases.TestEmail;
using Infrastructure.Data.Cache;
using Infrastructure.Data.Repositories;
using Infrastructure.Data.SqlServer;
using Infrastructure.Web;

namespace Plumbing
{
    public class DependencyContainer
    {
        private static DependencyContainer _instance;

        public Func<TestEmailResult> TestEmail { get; private set; }
       
        public Func<EditUserFormRequest, EditUserFormResult> EditUserForm { get; private set; }

        private DependencyContainer()
        {
            var storageProvider = new SqlServerStorageProvider();
            var cacheProvider = new CacheProvider();
            var cacheContainer = new CacheContainer(cacheProvider);
            var cacheBuster = new CacheBuster(cacheContainer);
            
            var bunchStorage = new SqlServerBunchStorage(storageProvider);
            var bunchRepository = new BunchRepository(bunchStorage, cacheContainer, cacheBuster);

            var userStorage = new SqlServerUserStorage(storageProvider);
            var userRepository = new UserRepository(userStorage, cacheContainer, cacheBuster);

            var messageSender = new MessageSender();

            TestEmail = () => TestEmailInteractor.Execute(messageSender);

            EditUserForm = (request) => EditUserFormInteractor.ExecuteStatic(userRepository, request);
        }

        public static DependencyContainer Instance
        {
            get { return _instance ?? (_instance = new DependencyContainer()); }
        } 
    }
}