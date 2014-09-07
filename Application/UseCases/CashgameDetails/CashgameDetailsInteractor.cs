using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.CashgameDetails
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
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(homegame, request.DateStr);

            if (cashgame == null)
            {
                throw new CashgameNotFoundException();
            }
            
            var isManager = auth.IsInRole(request.Slug, Role.Manager);
            var players = GetPlayers(playerRepository, cashgame);
            
            return new CashgameDetailsResult(homegame, cashgame, players, isManager);
        }

        private static IEnumerable<Player> GetPlayers(IPlayerRepository playerRepository, Cashgame cashgame)
        {
            var playerIds = cashgame.Results.Select(o => o.PlayerId).ToList();
            return playerRepository.GetList(playerIds);
        }
    }
}