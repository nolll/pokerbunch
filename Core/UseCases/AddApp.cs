using System;
using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class AddApp
    {
        private readonly IAppRepository _appRepository;
        private readonly IUserRepository _userRepository;

        public AddApp(IAppRepository appRepository, IUserRepository userRepository)
        {
            _appRepository = appRepository;
            _userRepository = userRepository;
        }

        public void Execute(Request request)
        {
            var appName = request.AppName;
            var apiKey = Guid.NewGuid().ToString();
            var user = _userRepository.GetByNameOrEmail(request.UserName);

            var app = new App(0, apiKey, appName, user.Id);

            _appRepository.Add(app);
        }

        public class Request
        {
            [Required(ErrorMessage = "User Name can't be empty")]
            public string UserName { get; private set; }

            [Required(ErrorMessage = "App Name can't be empty")]
            public string AppName { get; private set; }

            public Request(string userName, string appName)
            {
                UserName = userName;
                AppName = appName;
            }
        }
    }
}
