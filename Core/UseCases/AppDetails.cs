using Core.Services;

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
            var app = _appService.GetById(request.AppId);

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
