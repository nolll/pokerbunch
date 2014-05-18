using System;
using System.Globalization;
using Application.Factories;
using Core.Entities;
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
            var culture = CultureInfo.CreateSpecificCulture("sv-SE");
            var currency = new Currency(rawHomegame.CurrencySymbol, rawHomegame.CurrencyLayout, culture);

            return _homegameFactory.Create(
                rawHomegame.Id,
                rawHomegame.Slug,
                rawHomegame.DisplayName,
                rawHomegame.Description,
                rawHomegame.HouseRules,
                TimeZoneInfo.FindSystemTimeZoneById(rawHomegame.TimezoneName),
                rawHomegame.DefaultBuyin,
                currency);
        }
    }
}