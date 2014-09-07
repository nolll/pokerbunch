using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.Actions
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
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(homegame, request.DateStr);
            var player = playerRepository.GetById(request.PlayerId);
            var playerResult = cashgame.GetResult(player.Id);
            var isManager = auth.IsInRole(homegame.Slug, Role.Manager);

            return new ActionsResult(homegame, cashgame, player, isManager, playerResult);
        }
    }
}