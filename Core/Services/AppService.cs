using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;

namespace Core.Services
{
    public class AppService
    {
        private readonly IAppRepository _appRepository;

        public AppService(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public IList<App> List()
        {
            return _appRepository.List();
        }

        public IList<App> ListByUser(string userId)
        {
            return _appRepository.ListByUser(userId);
        }

        public App GetById(string id)
        {
            return _appRepository.GetById(id);
        }

        public App GetByAppKey(string appKey)
        {
            return _appRepository.GetByAppKey(appKey);
        }

        public string Add(App app)
        {
            return _appRepository.Add(app);
        }

        public void Update(App app)
        {
            _appRepository.Update(app);
        }
    }
}