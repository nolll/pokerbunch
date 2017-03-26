using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class AppListAll
    {
        private readonly IAppService _appService;

        public AppListAll(IAppService appService)
        {
            _appService = appService;
        }

        public Result Execute()
        {
            var apps = _appService.ListAll();

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