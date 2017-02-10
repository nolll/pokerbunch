using System.ComponentModel.DataAnnotations;
using Core.Repositories;

namespace Core.UseCases
{
    public class AddApp
    {
        private readonly IAppRepository _appRepository;

        public AddApp(IAppRepository appRepository)
        {
            _appRepository = appRepository;
        }

        public void Execute(Request request)
        {
            var appName = request.AppName;

            _appRepository.Add(appName);
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
