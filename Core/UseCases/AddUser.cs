using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class AddUser
    {
        private readonly IUserRepository _userRepository;

        public AddUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(Request request)
        {
            var user = new User("", request.UserName, request.DisplayName, string.Empty, request.Email);
            _userRepository.Add(user, request.Password);
        }

        public class Request
        {
            public string UserName { get; }
            public string DisplayName { get; }
            public string Email { get; }
            public string Password { get; }

            public Request(string userName, string displayName, string email, string password)
            {
                UserName = userName;
                DisplayName = displayName;
                Email = email;
                Password = password;
            }
        }
    }
}