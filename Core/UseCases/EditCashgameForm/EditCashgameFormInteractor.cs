using System;
using Core.Entities;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.EditCashgameForm
{
    public class EditCashgameFormInteractor
    {
        private readonly IBunchRepository _bunchRepository;
        private readonly ICashgameRepository _cashgameRepository;

        public EditCashgameFormInteractor(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository)
        {
            _bunchRepository = bunchRepository;
            _cashgameRepository = cashgameRepository;
        }

        public EditCashgameFormResult Execute(EditCashgameFormRequest request)
        {
            var bunch = _bunchRepository.GetBySlug(request.Slug);
            var cashgame = _cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            var startTime = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : DateTime.MinValue;
            
            var date = new Date(startTime);
            var cancelUrl = new CashgameDetailsUrl(bunch.Slug, cashgame.DateString);
            var deleteUrl = new DeleteCashgameUrl(bunch.Slug, cashgame.DateString);
            var location = cashgame.Location;
            var locations = _cashgameRepository.GetLocations(bunch.Id);

            return new EditCashgameFormResult(date, cancelUrl, deleteUrl, location, locations);
        }
    }
}