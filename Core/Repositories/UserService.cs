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
            return _userRepository.Get(id);
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            var ids = _userRepository.Find(nameOrEmail);
            if (ids.Any())
                return _userRepository.Get(ids.First());
            return null;
        }

        public IList<User> GetList()
        {
            var ids = _userRepository.Find();
            return _userRepository.Get(ids);
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
