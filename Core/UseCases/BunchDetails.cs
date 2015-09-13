using Core.Entities;
using Core.Repositories;
using Core.Services;

namespace Core.UseCases
{
    public class BunchDetails
    {
        private readonly BunchService _bunchService;
        private readonly UserService _userService;
        private readonly PlayerService _playerService;

        public BunchDetails(BunchService bunchService, UserService userService, PlayerService playerService)
        {
            _bunchService = bunchService;
            _userService = userService;
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.GetBySlug(request.Slug);
            var user = _userService.GetByNameOrEmail(request.UserName);
            var player = _playerService.GetByUserId(bunch.Id, user.Id);
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