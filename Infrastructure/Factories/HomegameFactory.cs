using System;
using System.Collections.Generic;
using System.Linq;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    public class HomegameFactory : IHomegameFactory
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

        public IList<Homegame> CreateList(IEnumerable<RawHomegame> rawHomegames)
        {
            return rawHomegames.Select(Create).ToList();
        }
    }
}