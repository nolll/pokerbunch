using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.Actions
{
    public class ActionsInteractor : IActionsInteractor
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuth _auth;

        public ActionsInteractor(
            IHomegameRepository homegameRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            IAuth auth)
        {
            _homegameRepository = homegameRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _auth = auth;
        }

        public ActionsResult Execute(ActionsRequest request)
        {
            var homegame = _homegameRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, request.DateStr);
            var player = _playerRepository.GetById(request.PlayerId);
            var playerResult = cashgame.GetResult(player.Id);
            var isManager = _auth.IsInRole(homegame.Slug, Role.Manager);

            return new ActionsResult(homegame, cashgame, player, isManager, playerResult);
        }
    }
}