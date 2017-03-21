using Core.Entities;
using Core.Repositories;

namespace Core.UseCases
{
    public class EditUser
    {
        private readonly IUserRepository _userRepository;

        public EditUser(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Result Execute(Request request)
        {
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var userToSave = GetUser(user, request);

            _userRepository.Update(userToSave);

            return new Result(userToSave.UserName);
        }

        private static User GetUser(User user, Request request)
        {
            return new User(
                user.Id,
                user.UserName,
                request.DisplayName,
                request.RealName,
                request.Email,
                user.GlobalRole,
                user.EncryptedPassword,
                user.Salt);
        }

        public class Request
        {
            public string UserName { get; }
            public string DisplayName { get; }
            public string RealName { get; }
            public string Email { get; }

            public Request(string userName, string displayName, string realName, string email)
            {
                UserName = userName;
                DisplayName = displayName;
                RealName = realName;
                Email = email;
            }
        }

        public class Result
        {
            public string UserName { get; private set; }

            public Result(string userName)
            {
                UserName = userName;
            }
        }
    }
}
