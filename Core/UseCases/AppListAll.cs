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
            public IList<Item> Items { get; }

            public Result(IEnumerable<App> apps)
            {
                Items = apps.Select(o => new Item(o)).ToList();
            }
        }

        public class Item
        {
            public string AppId { get; }
            public string AppKey { get; }
            public string AppName { get; }

            public Item(App app)
            {
                AppId = app.Id;
                AppKey = app.AppKey;
                AppName = app.Name;
            }
        }
    }
}