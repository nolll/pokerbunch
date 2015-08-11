using System.Collections.Generic;
using System.Linq;
using Core.Repositories;
using Core.Urls;

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

        public class UserListResult
        {
            public IList<UserListItem> Users { get; private set; }

            public UserListResult(IList<UserListItem> userItems)
            {
                Users = userItems;
            }
        }

        public class UserListItem
        {
            public string DisplayName { get; private set; }
            public Url Url { get; private set; }

            public UserListItem(string displayName, string userName)
            {
                DisplayName = displayName;
                Url = new UserDetailsUrl(userName);
            }
        }
    }
}