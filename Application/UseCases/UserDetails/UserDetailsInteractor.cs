using Application.Services;
using Core.Repositories;

namespace Application.UseCases.UserDetails
{
    public class UserDetailsInteractor : IUserDetailsInteractor
    {
        private readonly IAuth _auth;
        private readonly IUserRepository _userRepository;

        public UserDetailsInteractor(
            IAuth auth,
            IUserRepository userRepository)
        {
            _auth = auth;
            _userRepository = userRepository;
        }

        public UserDetailsResult Execute(UserDetailsRequest request)
        {
            var currentUser = _auth.CurrentUser;
            var displayUser = _userRepository.GetByNameOrEmail(request.UserName);

            return new UserDetailsResult(currentUser, displayUser);
        }
    }
}