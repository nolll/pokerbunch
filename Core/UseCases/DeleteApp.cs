using Core.Services;

namespace Core.UseCases
{
    public class DeleteApp
    {
        private readonly IAppService _appService;

        public DeleteApp(IAppService appService)
        {
            _appService = appService;
        }

        public Result Execute(Request request)
        {
            _appService.Delete(request.AppId);

            return new Result();
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
        }
    }
}