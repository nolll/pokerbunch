using System;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    internal class HomegameFactory : IHomegameFactory
    {
        public Homegame Create(RawHomegame rawHomegame)
        {
            return new Homegame
                {
                    Id = rawHomegame.Id,
                    Slug = rawHomegame.Slug,
                    DisplayName = rawHomegame.DisplayName,
                    Description = rawHomegame.Description,
                    HouseRules = rawHomegame.HouseRules,
                    Currency = new CurrencySettings(rawHomegame.CurrencySymbol, rawHomegame.CurrencyLayout),
                    Timezone = TimeZoneInfo.FindSystemTimeZoneById(rawHomegame.TimezoneName),
                    DefaultBuyin = rawHomegame.DefaultBuyin,
                    CashgamesEnabled = rawHomegame.CashgamesEnabled,
                    TournamentsEnabled = rawHomegame.TournamentsEnabled,
                    VideosEnabled = rawHomegame.VideosEnabled
                };
        }

    }
}