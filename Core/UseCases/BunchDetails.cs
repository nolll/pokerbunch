using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class BunchDetails
    {
        private readonly IBunchService _bunchService;

        public BunchDetails(IBunchService bunchService)
        {
            _bunchService = bunchService;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchService.Get(request.Slug);

            var id = bunch.Id;
            var bunchName = bunch.DisplayName;
            var description = bunch.Description;
            var houseRules = bunch.HouseRules;
            var canEdit = RoleHandler.IsInRole(bunch.Role, Role.Manager);

            return new Result(id, bunchName, description, houseRules, canEdit);
        }

        public class Request
        {
            public string Slug { get; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }

        public class Result
        {
            public string Id { get; }
            public string BunchName { get; }
            public string Description { get; }
            public string HouseRules { get; }
            public bool CanEdit { get; }

            public Result(string id, string bunchName, string description, string houseRules, bool canEdit)
            {
                Id = id;
                BunchName = bunchName;
                Description = description;
                HouseRules = houseRules;
                CanEdit = canEdit;
            }
        }
    }
}