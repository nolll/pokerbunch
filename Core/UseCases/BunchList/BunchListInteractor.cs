using System.Collections.Generic;
using Core.Entities;
using Core.Repositories;

namespace Core.UseCases.BunchList
{
    public class BunchListInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;

        public BunchListInteractor(IBunchRepository bunchRepository, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
        }

        public BunchListResult Execute()
        {
            var bunches = _bunchRepository.GetList();
            return new BunchListResult(bunches);
        }

        public BunchListResult Execute(BunchListRequest request)
        {
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var homegames = user != null ? _bunchRepository.GetByUserId(user.Id) : new List<Bunch>();
            
            return new BunchListResult(homegames);
        }
    }
}