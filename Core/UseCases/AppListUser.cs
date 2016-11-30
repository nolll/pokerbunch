using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class AppListUser
    {
        private readonly IAppRepository _appRepository;

        public AppListUser(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public Result Execute()
        {
            var apps = _appRepository.List();

            return new Result(apps);
        }

        public class Result
        {
            public IList<Item> Items { get; private set; }

            public Result(IEnumerable<App> apps)
            {
                Items = apps.Select(o => new Item(o)).ToList();
            }
        }

        public class Item
        {
            public string AppId { get; private set; }
            public string AppKey { get; private set; }
            public string AppName { get; private set; }

            public Item(App app)
            {
                AppId = app.Id;
                AppKey = app.AppKey;
                AppName = app.Name;
            }
        }
    }
}