using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class BunchDetails
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPlayerRepository _playerRepository;

        public BunchDetails(IBunchRepository bunchRepository, IUserRepository userRepository, IPlayerRepository playerRepository)
        {
            _bunchRepository = bunchRepository;
            _userRepository = userRepository;
            _playerRepository = playerRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var user = _userRepository.GetByNameOrEmail(request.UserName);
            var player = _playerRepository.GetByUserId(bunch.Id, user.Id);
            RoleHandler.RequirePlayer(user, player);

            var slug = bunch.Slug;
            var bunchName = bunch.DisplayName;
            var description = bunch.Description;
            var houseRules = bunch.HouseRules;
            var canEdit = RoleHandler.IsInRole(user, player, Role.Manager);

            return new Result(slug, bunchName, description, houseRules, canEdit);
        }

        public class Request
        {
            public string UserName { get; private set; }
            public string Slug { get; private set; }

            public Request(string userName, string slug)
            {
                UserName = userName;
                Slug = slug;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }
            public string BunchName { get; private set; }
            public string Description { get; private set; }
            public string HouseRules { get; private set; }
            public bool CanEdit { get; private set; }

            public Result(string slug, string bunchName, string description, string houseRules, bool canEdit)
            {
                Slug = slug;
                BunchName = bunchName;
                Description = description;
                HouseRules = houseRules;
                CanEdit = canEdit;
            }
        }
    }
}