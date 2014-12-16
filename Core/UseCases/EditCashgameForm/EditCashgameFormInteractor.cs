using System;
using Core.Entities;
using Core.Repositories;
using Core.Urls;

namespace Core.UseCases.EditCashgameForm
{
    public class EditCashgameFormInteractor
    {
        public static EditCashgameFormResult Execute(IBunchRepository bunchRepository, ICashgameRepository cashgameRepository, EditCashgameFormRequest request)
        {
            var bunch = bunchRepository.GetBySlug(request.Slug);
            var cashgame = cashgameRepository.GetByDateString(bunch.Id, request.DateStr);
            var startTime = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : DateTime.MinValue;
            
            var date = new Date(startTime);
            var cancelUrl = new CashgameDetailsUrl(bunch.Slug, cashgame.DateString);
            var deleteUrl = new DeleteCashgameUrl(bunch.Slug, cashgame.DateString);
            var location = cashgame.Location;
            var locations = cashgameRepository.GetLocations(bunch.Id);

            return new EditCashgameFormResult(date, cancelUrl, deleteUrl, location, locations);
        }
    }
}