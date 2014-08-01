using System.Collections.Generic;
using System.Linq;
using Application.Exceptions;
using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.CashgameDetails
{
    public class CashgameDetailsInteractor : ICashgameDetailsInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IAuth _auth;
        private readonly IPlayerRepository _playerRepository;

        public CashgameDetailsInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IAuth auth,
            IPlayerRepository playerRepository)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _auth = auth;
            _playerRepository = playerRepository;
        }

        public CashgameDetailsResult Execute(CashgameDetailsRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, request.DateStr);

            if (cashgame == null)
            {
                throw new CashgameNotFoundException();
            }
            
            var isManager = _auth.IsInRole(request.Slug, Role.Manager);
            var players = GetPlayers(cashgame);
            
            return new CashgameDetailsResult(homegame, cashgame, players, isManager);
        }

        private IList<Player> GetPlayers(Cashgame cashgame)
        {
            var playerIds = cashgame.Results.Select(o => o.PlayerId).ToList();
            return _playerRepository.GetList(playerIds);
        }
    }
}