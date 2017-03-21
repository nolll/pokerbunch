using Core.Repositories;

namespace Core.UseCases
{
    public class ChangePassword
    {
        private readonly IUserRepository _userRepository;

        public ChangePassword(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(Request request)
        {
            _userRepository.ChangePassword(request.OldPassword, request.NewPassword, request.Repeat);
        }

        public class Request
        {
            public string OldPassword { get; }
            public string NewPassword { get; }
            public string Repeat { get; }

            public Request(string oldPassword, string newPassword, string repeat)
            {
                OldPassword = oldPassword;
                NewPassword = newPassword;
                Repeat = repeat;
            }
        }
    }
}
