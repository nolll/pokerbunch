using System.Collections.Generic;
using Core.Exceptions;
using Core.Repositories;

namespace Core.UseCases
{
    public class AddCashgameForm
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public AddCashgameForm(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public Result Execute(Request request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var runningGame = _cashgameRepository.GetRunning(bunch.Id);
            if (runningGame != null)
            {
                throw new CashgameRunningException();
            }
            var locations = _cashgameRepository.GetLocations(bunch.Id);
            return new Result(locations);
        }

        public class Request
        {
            public string Slug { private set; get; }

            public Request(string slug)
            {
                Slug = slug;
            }
        }

        public class Result
        {
            public IList<string> Locations { get; private set; }

            public Result(IList<string> locations)
            {
                Locations = locations;
            }
        }
    }
}