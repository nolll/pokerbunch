﻿using Core.Repositories;

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
            var homegames = _bunchRepository.GetList();

            return new BunchListResult(homegames);
        }

        public BunchListResult Execute(BunchListRequest request)
        {
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var homegames = _bunchRepository.GetByUserId(user.Id);
            
            return new BunchListResult(homegames);
        }
    }
}