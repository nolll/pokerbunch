using System.ComponentModel.DataAnnotations;
using Core.Entities;
using Core.Repositories;
using Core.Urls;
using ValidationException = Core.Exceptions.ValidationException;

namespace Core.UseCases
{
    public class AddCashgame
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public AddCashgame(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var validator = new Validator(request);

            if (!validator.IsValid)
                throw new ValidationException(validator);

            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = new Cashgame(bunch.Id, request.Location, GameStatus.Running);
            _cashgameRepository.AddGame(bunch, cashgame);

            return new Result(request.Slug);
        }

        public class Request
        {
            public string Slug { get; private set; }
            [Required(ErrorMessage = "Please select or enter a location")]
            public string Location { get; private set; }

            public Request(string slug, string location)
            {
                Slug = slug;
                Location = location;
            }
        }

        public class Result
        {
            public Url ReturnUrl { get; private set; }

            public Result(string slug)
            {
                ReturnUrl = new RunningCashgameUrl(slug);
            }
        }
    }
}