﻿using Core.Repositories;

namespace Core.UseCases
{
    public class AppDetails
    {
        private readonly IAppRepository _appRepository;
        
        public AppDetails(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public Result Execute(Request request)
        {
            var app = _appRepository.GetById(request.AppId);

            return new Result(app.Id, app.AppKey, app.Name);
        }

        public class Request
        {
            public string AppId { get; }

            public Request(string appId)
            {
                AppId = appId;
            }
        }

        public class Result
        {
            public string AppId { get; private set; }
            public string AppKey { get; private set; }
            public string AppName { get; private set; }

            public Result(string appId, string appKey, string appName)
            {
                AppId = appId;
                AppKey = appKey;
                AppName = appName;
            }
        }
    }
}
