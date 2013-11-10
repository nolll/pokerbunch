using System;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Factories
{
    public class HomegameFactory : IHomegameFactory
    {
        public Homegame Create(RawHomegame rawHomegame)
        {
            return new Homegame
                (
                    rawHomegame.Id,
                    rawHomegame.Slug,
                    rawHomegame.DisplayName,
                    rawHomegame.Description,
                    rawHomegame.HouseRules,
                    TimeZoneInfo.FindSystemTimeZoneById(rawHomegame.TimezoneName),
                    rawHomegame.DefaultBuyin,
                    new CurrencySettings(rawHomegame.CurrencySymbol, rawHomegame.CurrencyLayout)
                );
        }

    }
}