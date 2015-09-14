﻿using Core.Services;

namespace Core.UseCases
{
    public class AppDetails
    {
        private readonly AppService _appService;
        
        public AppDetails(AppService appService)
        {
            _appService = appService;
        }

        public Result Execute(Request request)
        {
            var app = _appService.Get(request.AppId);

            return new Result(app.Id, app.AppKey, app.Name);
        }

        public class Request
        {
            public int AppId { get; private set; }

            public Request(int appId)
            {
                AppId = appId;
            }
        }

        public class Result
        {
            public int AppId { get; private set; }
            public string AppKey { get; private set; }
            public string AppName { get; private set; }

            public Result(int appId, string appKey, string appName)
            {
                AppId = appId;
                AppKey = appKey;
                AppName = appName;
            }
        }
    }
}