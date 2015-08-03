using Core.Entities;
using Core.Repositories;
using Core.Services;
using Core.Urls;

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
            
            var bunchName = bunch.DisplayName;
            var description = bunch.Description;
            var houseRules = bunch.HouseRules;
            var editBunchUrl = new EditBunchUrl(bunch.Slug);
            var canEdit = RoleHandler.IsInRole(user, player, Role.Manager);

            return new Result(bunchName, description, houseRules, editBunchUrl, canEdit);
        }

        public class Request
        {
            public string Slug { get; private set; }
            public string UserName { get; private set; }

            public Request(string slug, string userName)
            {
                Slug = slug;
                UserName = userName;
            }
        }

        public class Result
        {
            public string BunchName { get; private set; }
            public string Description { get; private set; }
            public string HouseRules { get; private set; }
            public Url EditBunchUrl { get; private set; }
            public bool CanEdit { get; private set; }

            public Result(string bunchName, string description, string houseRules, Url editBunchUrl, bool canEdit)
            {
                BunchName = bunchName;
                Description = description;
                HouseRules = houseRules;
                EditBunchUrl = editBunchUrl;
                CanEdit = canEdit;
            }
        }
    }
}