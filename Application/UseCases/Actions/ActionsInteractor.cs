using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.Actions
{
    public class ActionsInteractor : IActionsInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IAuth _auth;

        public ActionsInteractor(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            IAuth auth)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
            _playerRepository = playerRepository;
            _auth = auth;
        }

        public ActionsResult Execute(ActionsRequest request)
        {
            var homegame = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(homegame, request.DateStr);
            var player = _playerRepository.GetById(request.PlayerId);
            var playerResult = cashgame.GetResult(player.Id);
            var isManager = _auth.IsInRole(homegame.Slug, Role.Manager);

            return new ActionsResult(homegame, cashgame, player, isManager, playerResult);
        }
    }
}