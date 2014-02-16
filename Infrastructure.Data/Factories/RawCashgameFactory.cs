using System;
using Application.Services;
using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Factories.Interfaces;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public class RawCashgameFactory : IRawCashgameFactory
    {
        private readonly ITimeProvider _timeProvider;

        public RawCashgameFactory(ITimeProvider timeProvider)
        {
            _timeProvider = timeProvider;
        }

        public RawCashgame Create(IStorageDataReader reader)
        {
            var location = reader.GetStringValue("Location");
            if (location == "")
            {
                location = null;
            }

            return new RawCashgame
                {
                    Id = reader.GetIntValue("GameID"),
                    Location = location,
                    Status = reader.GetIntValue("Status"),
                    Date = TimeZoneInfo.ConvertTimeToUtc(reader.GetDateTimeValue("Date")),
                };
        }

        public RawCashgame Create(Cashgame cashgame, GameStatus? status = null)
        {
            return new RawCashgame
            {
                Id = cashgame.Id,
                Location = cashgame.Location,
                Status = status.HasValue ? (int)status.Value : (int)cashgame.Status,
                Date = cashgame.StartTime.HasValue ? cashgame.StartTime.Value : _timeProvider.GetTime(),
            };
        }
    }
}