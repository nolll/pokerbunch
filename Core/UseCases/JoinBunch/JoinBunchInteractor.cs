using System.Collections.Generic;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases.JoinBunch
{
    public static class JoinBunchInteractor
    {
        public static JoinBunchResult Execute(IAuth auth, IBunchRepository bunchRepository, IPlayerRepository playerRepository, JoinBunchRequest request)
        {
            var validator = new Validator(request);
            if(!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = bunchRepository.GetBySlug(request.Slug);
            var players = playerRepository.GetList(bunch.Id);
            var player = GetMatchedPlayer(players, request.Code);
            if (player != null && player.IsUser)
            {
                playerRepository.JoinHomegame(player, bunch, auth.CurrentIdentity.UserId);
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
