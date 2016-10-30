using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Services;

namespace Core.Repositories
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetById(string id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            return _userRepository.GetByNameOrEmail(nameOrEmail);
        }

        public IList<User> List()
        {
            return _userRepository.List();
        }
        
        public void Update(User user)
        {
            _userRepository.Update(user);
        }

        public string Add(User user)
        {
            return _userRepository.Add(user);
        }
    }
}
