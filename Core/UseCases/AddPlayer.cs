using Core.Entities;
using Core.Services;

namespace Core.UseCases
{
    public class AddPlayer
    {
        private readonly IPlayerService _playerService;

        public AddPlayer(IPlayerService playerService)
        {
            _playerService = playerService;
        }

        public Result Execute(Request request)
        {
            var player = Player.NewWithoutUser(request.Slug, request.Name);
            _playerService.Add(player);

            return new Result(request.Slug);
        }

        public class Request
        {
            public string Slug { get; }
            public string Name { get; }

            public Request(string slug, string name)
            {
                Slug = slug;
                Name = name;
            }
        }

        public class Result
        {
            public string Slug { get; private set; }

            public Result(string slug)
            {
                Slug = slug;
            }
        }
    }
}