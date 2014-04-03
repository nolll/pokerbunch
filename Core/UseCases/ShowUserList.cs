using System.Collections.Generic;
using System.Linq;
using Core.Repositories;

namespace Core.UseCases
{
    public class ShowUserList : IShowUserList
    {
        private readonly IUserRepository _userRepository;

        public ShowUserList(IUserRepository userRepository)
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

        private IList<UserItem> GetUserItems()
        {
            var users = _userRepository.GetList();
            return users.Select(o => new UserItem(o.UserName, o.UserName)).ToList();
        } 
    }
}