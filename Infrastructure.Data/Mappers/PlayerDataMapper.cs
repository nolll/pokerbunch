using Application.Factories;
using Core.Entities;
using Core.Repositories;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public class PlayerDataMapper : IPlayerDataMapper
    {
        private readonly IUserRepository _userRepository;
        private readonly IPlayerFactory _playerFactory;

        public PlayerDataMapper(
            IUserRepository userRepository,
            IPlayerFactory playerFactory)
        {
            _userRepository = userRepository;
            _playerFactory = playerFactory;
        }

        public Player Create(RawPlayer rawPlayer)
        {
            return _playerFactory.Create(
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