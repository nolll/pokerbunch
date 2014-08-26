using Application.Factories;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public class PlayerDataMapper : IPlayerDataMapper
    {
        private readonly IUserRepository _userRepository;

        public PlayerDataMapper(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Player Create(RawPlayer rawPlayer)
        {
            return PlayerFactory.Create(
                rawPlayer.Id,
                rawPlayer.UserId,
                GetDisplayName(rawPlayer),
                (Role)rawPlayer.Role);
        }

        private string GetDisplayName(RawPlayer rawPlayer)
        {
            if (rawPlayer.IsUser && rawPlayer.DisplayName == null)
            {
                var user = _userRepository.GetById(rawPlayer.UserId);
                return user.DisplayName;
            }
            return rawPlayer.DisplayName;
        }
    }
}