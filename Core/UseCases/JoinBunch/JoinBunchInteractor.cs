using System.Collections.Generic;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.JoinBunch
{
    public class JoinBunchInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IUserRepository _userRepository;

        public JoinBunchInteractor(IBunchRepository bunchRepository, IPlayerRepository playerRepository, IUserRepository userRepository)
        {
            _bunchRepository = bunchRepository;
            _playerRepository = playerRepository;
            _userRepository = userRepository;
        }

        public JoinBunchResult Execute(JoinBunchRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var players = _playerRepository.GetList(bunch.Id);
            var player = GetMatchedPlayer(players, request.Code);
            if (player != null)
            {
                var user = _userRepository.GetByNameOrEmail(request.UserName);
                _playerRepository.JoinHomegame(player, bunch, user.Id);
            }
            else
            {
                throw new InvalidJoinCodeException();
            }

            var returnUrl = new JoinBunchConfirmationUrl(request.Slug);
            return new JoinBunchResult(returnUrl);
        }
        
        private static Player GetMatchedPlayer(IEnumerable<Player> players, string postedCode)
        {
            foreach (var player in players)
            {
                var code = InvitationCodeCreator.GetCode(player);
                if (code == postedCode)
                    return player;
            }
            return null;
        }
    }
}
