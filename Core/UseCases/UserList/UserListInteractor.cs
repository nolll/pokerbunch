using System.Collections.Generic;
using System.Linq;
using Core.Repositories;

namespace Core.UseCases.UserList
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
            return new UserListResult
                {
                    Users = GetUserItems()
                };
        }

        private IList<UserListItem> GetUserItems()
        {
            var users = _userRepository.GetList();
            return users.Select(o => new UserListItem(o.UserName, o.UserName)).ToList();
        }
    }
}