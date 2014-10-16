using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.Actions
{
    public static class ActionsInteractor
    {
        public static ActionsOutput Execute(
            IBunchRepository bunchRepository,
            ICashgameRepository cashgameRepository,
            IPlayerRepository playerRepository,
            IAuth auth,
            ActionsInput input)
        {
            var bunch = bunchRepository.GetBySlug(input.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch, input.DateStr);
            var player = playerRepository.GetById(input.PlayerId);
            var playerResult = cashgame.GetResult(player.Id);
            var isManager = auth.IsInRole(bunch.Slug, Role.Manager);

            return new ActionsOutput(bunch, cashgame, player, isManager, playerResult);
        }
    }
}