using System.ComponentModel.DataAnnotations;
using Core.Services;

namespace Core.UseCases
{
    public class AddApp
    {
        private readonly IAppService _appService;

        public AddApp(IAppService appService)
        {
            _appService = appService;
        }

        public void Execute(Request request)
        {
            var appName = request.AppName;

            _appService.Add(appName);
        }

        public class Request
        {
            [Required(ErrorMessage = "App Name can't be empty")]
            public string AppName { get; }

            public Request(string appName)
            {
                AppName = appName;
            }
        }
    }
}
