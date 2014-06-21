using Core.Repositories;

namespace Application.UseCases.UserList
{
    public class UserListInteractor : IUserListInteractor
    {
        private readonly IUserRepository _userRepository;

        public UserListInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserListResult Execute()
        {
            var users = _userRepository.GetList();

            return new UserListResult(users);
        }
    }
}