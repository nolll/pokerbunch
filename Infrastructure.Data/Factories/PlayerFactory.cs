using Core.Classes;
using Core.Repositories;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;

namespace Infrastructure.Data.Factories{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly IUserRepository _userRepository;

        public PlayerFactory(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Player Create(RawPlayer rawPlayer)
        {
            return new Player
            (
                rawPlayer.Id,
                rawPlayer.UserId,
                GetDisplayName(rawPlayer),
                (Role)rawPlayer.Role
            );
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