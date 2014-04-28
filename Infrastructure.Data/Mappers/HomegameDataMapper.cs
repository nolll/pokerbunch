using System;
using Application.Factories;
using Core.Classes;
using Infrastructure.Data.Classes;

namespace Infrastructure.Data.Mappers
{
    public class HomegameDataMapper : IHomegameDataMapper
    {
        private readonly IHomegameFactory _homegameFactory;

        public HomegameDataMapper(IHomegameFactory homegameFactory)
        {
            _homegameFactory = homegameFactory;
        }

        public Homegame Map(RawHomegame rawHomegame)
        {
            return _homegameFactory.Create(
                rawHomegame.Id,
                rawHomegame.Slug,
                rawHomegame.DisplayName,
                rawHomegame.Description,
                rawHomegame.HouseRules,
                TimeZoneInfo.FindSystemTimeZoneById(rawHomegame.TimezoneName),
                rawHomegame.DefaultBuyin,
                new Currency(rawHomegame.CurrencySymbol, rawHomegame.CurrencyLayout));
        }
    }
}