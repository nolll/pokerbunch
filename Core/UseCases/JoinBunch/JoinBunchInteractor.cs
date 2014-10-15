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
            var players = playerRepository.GetList(bunch);
            var player = GetMatchedPlayer(bunch, players, request.Code);
            if (player != null && player.IsUser)
            {
                var user = auth.CurrentUser;
                playerRepository.JoinHomegame(player, bunch, user);
            }
            else
            {
                throw new InvalidJoinCodeException();
            }

            var returnUrl = new JoinBunchConfirmationUrl(request.Slug);
            return new JoinBunchResult(returnUrl);
        }
        
        private static Player GetMatchedPlayer(Bunch bunch, IList<Player> players, string postedCode)
        {
            foreach (var player in players)
            {
                var code = InvitationCodeCreator.GetCode(player);
                if (code == postedCode)
                {
                    return player;
                }
            }
            return null;
        }
    }
}
