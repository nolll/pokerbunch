using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class AppList
    {
        private readonly IAppRepository _appRepository;
        private readonly IUserRepository _userRepository;

        public AppList(IAppRepository appRepository, IUserRepository userRepository)
        {
            _appRepository = appRepository;
            _userRepository = userRepository;
        }

        public Result Execute(AllAppsRequest request)
        {
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            RoleHandler.RequireAdmin(user);
            var apps = _appRepository.ListApps();

            return new Result(apps);
        }

        public Result Execute(UserAppsRequest request)
        {
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var apps = _appRepository.ListApps(user.Id);

            return new Result(apps);
        }

        public abstract class Request
        {
            public string UserName { get; private set; }

            protected Request(string userName)
            {
                UserName = userName;
            }
        }

        public class AllAppsRequest : Request
        {
            public AllAppsRequest(string userName)
                : base(userName)
            {
            }
        }

        public class UserAppsRequest : Request
        {
            public UserAppsRequest(string userName)
                : base(userName)
            {
            }
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
            public int AppId { get; private set; }
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