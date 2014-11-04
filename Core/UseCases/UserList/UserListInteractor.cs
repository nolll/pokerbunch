using System.Linq;
using Core.Repositories;

namespace Core.UseCases.UserList
{
    public static  class UserListInteractor
    {
        public static UserListResult Execute(IUserRepository userRepository)
        {
            var users = userRepository.GetList();
            var userItems = users.Select(o => new UserListItem(o.DisplayName, o.UserName)).ToList();

            return new UserListResult(userItems);
        }
    }
}