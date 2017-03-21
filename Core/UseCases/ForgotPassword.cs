using Core.Repositories;

namespace Core.UseCases
{
    public class ForgotPassword
    {
        private readonly IUserRepository _userRepository;

        public ForgotPassword(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(Request request)
        {
            _userRepository.ResetPassword(request.Email);
        }

        public class Request
        {
            public string Email { get; }

            public Request(string email)
            {
                Email = email;
            }
        }
    }
}