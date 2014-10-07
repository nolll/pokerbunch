using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Exceptions;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.CashgameDetails
{
    public static class CashgameDetailsInteractor
    {
        public static CashgameDetailsResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IAuth auth,
            IPlayerRepository playerRepository,
            CashgameDetailsRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch, request.DateStr);

            if (cashgame == null)
            {
                throw new CashgameNotFoundException();
            }
            
            var isManager = auth.IsInRole(request.Slug, Role.Manager);
            var players = GetPlayers(playerRepository, cashgame);

            return new CashgameDetailsResult(bunch, cashgame, players, isManager);
        }

        private static IEnumerable<Player> GetPlayers(IPlayerRepository playerRepository, Cashgame cashgame)
        {
            var playerIds = cashgame.Results.Select(o => o.PlayerId).ToList();
            return playerRepository.GetList(playerIds);
        }
    }
}