using System.Collections.Generic;
using System.Linq;
using Core.Repositories;

namespace Core.UseCases.ShowUserList
{
    public class ShowUserListInteractor : IShowUserListInteractor
    {
        private readonly IUserRepository _userRepository;

        public ShowUserListInteractor(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ShowUserListResult Execute()
        {
            return new ShowUserListResult
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