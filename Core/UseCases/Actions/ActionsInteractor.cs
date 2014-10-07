using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.Actions
{
    public static class ActionsInteractor
    {
        public static ActionsResult Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            IAuth auth,
            ActionsRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch, request.DateStr);
            var player = playerRepository.GetById(request.PlayerId);
            var playerResult = cashgame.GetResult(player.Id);
            var isManager = auth.IsInRole(bunch.Slug, Role.Manager);

            return new ActionsResult(bunch, cashgame, player, isManager, playerResult);
        }
    }
}