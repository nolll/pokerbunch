using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases.PlayerList
{
    public static class PlayerListInteractor
    {
        public static PlayerListResult Execute(
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository,
            IAuth auth,
            PlayerListRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var players = playerRepository.GetList(bunch.Id);
            var isManager = auth.IsInRole(request.Slug, Role.Manager);

            return new PlayerListResult(bunch, players, isManager);
        }
    }
}