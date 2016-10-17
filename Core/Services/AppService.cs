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

        public IList<App> ListApps()
        {
            var ids = _appRepository.Find();
            return _appRepository.GetList(ids);
        }

        public IList<App> ListApps(string userId)
        {
            var ids = _appRepository.FindByUser(userId);
            return _appRepository.GetList(ids);
        }

        public App GetById(string id)
        {
            return _appRepository.Get(id);
        }

        public App GetByAppKey(string appKey)
        {
            var ids = _appRepository.FindByAppKey(appKey);
            if(ids.Count == 0)
                throw new AppNotFoundException();
            return _appRepository.Get(ids.First());
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