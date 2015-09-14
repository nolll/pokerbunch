﻿using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class AppList
    {
        private readonly AppService _appService;
        private readonly UserService _userService;

        public AppList(AppService appService, UserService userService)
        {
            _appService = appService;
            _userService = userService;
        }

        public Result Execute(AllAppsRequest request)
        {
            var user = _userService.GetByNameOrEmail(request.CurrentUserName);
            RoleHandler.RequireAdmin(user);
            var apps = _appService.ListApps();

            return new Result(apps);
        }

        public Result Execute(UserAppsRequest request)
        {
            var user = _userService.GetByNameOrEmail(request.CurrentUserName);
            var apps = _appService.ListApps(user.Id);

            return new Result(apps);
        }

        public abstract class Request
        {
            public string CurrentUserName { get; private set; }

            protected Request(string currentUserName)
            {
                CurrentUserName = currentUserName;
            }
        }

        public class AllAppsRequest : Request
        {
            public AllAppsRequest(string currentUserName)
                : base(currentUserName)
            {
            }
        }

        public class UserAppsRequest : Request
        {
            public UserAppsRequest(string currentUserName)
                : base(currentUserName)
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