using System.Linq;
using Core.Repositories;

namespace Core.UseCases.UserList
{
    public class UserListInteractor
    {
        private readonly IUserRepository _userRepository;

        public UserListInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public UserListResult Execute()
        {
            var users = _userRepository.GetList();
            var userItems = users.Select(o => new UserListItem(o.DisplayName, o.UserName)).ToList();

            return new UserListResult(userItems);
        }
    }
}