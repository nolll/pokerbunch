using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;

namespace Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public User GetByNameOrEmail(string nameOrEmail)
        {
            var ids = _userRepository.Search(nameOrEmail);
            if (ids.Any())
                return _userRepository.GetById(ids.First());
            return null;
        }

        public IList<User> GetList()
        {
            var ids = _userRepository.Search();
            return _userRepository.Get(ids);
        }
        
        public bool Save(User user)
        {
            return _userRepository.Save(user);
        }

        public int Add(User user)
        {
            return _userRepository.Add(user);
        }
    }
}
