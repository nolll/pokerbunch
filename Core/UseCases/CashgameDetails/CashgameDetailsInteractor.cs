using System.Collections.Generic;
using System.Linq;
using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.CashgameDetails
{
    public class CashgameDetailsInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;

        public CashgameDetailsInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, IAuth auth, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _auth = auth;
            _playerRepository = playerRepository;
        }

        public CashgameDetailsResult Execute(CashgameDetailsRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            var isManager = _auth.IsInRole(request.Slug, Role.Manager);
            var players = GetPlayers(_playerRepository, cashgame);

            return new CashgameDetailsResult(bunch, cashgame, players, isManager);
        }

        private static IEnumerable<Player> GetPlayers(IPlayerRepository playerRepository, Cashgame cashgame)
        {
            var playerIds = cashgame.Results.Select(o => o.PlayerId).ToList();
            return playerRepository.GetList(playerIds);
        }
    }
}