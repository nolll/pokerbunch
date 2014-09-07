using Application.Services;
using Core.Entities;
using Core.Repositories;

namespace Application.UseCases.PlayerList
{
    public static class PlayerListInteractor
    {
        public static PlayerListResult Execute(
            IBunchRepository bunchRepository,
            IPlayerRepository playerRepository,
            IAuth auth,
            PlayerListRequest request)
        {
            var homegame = bunchRepository.GetBySlug(request.Slug);
            var players = playerRepository.GetList(homegame);
            var isManager = auth.IsInRole(request.Slug, Role.Manager);

            return new PlayerListResult(homegame, players, isManager);
        }
    }
}