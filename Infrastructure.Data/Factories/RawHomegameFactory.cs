using Core.Entities;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public static class RawHomegameFactory
    {
        public static RawBunch Create(IStorageDataReader reader)
        {
            return new RawBunch
            {
                Id = reader.GetIntValue("HomegameID"),
                Slug = reader.GetStringValue("Name"),
                DisplayName = reader.GetStringValue("DisplayName"),
                Description = reader.GetStringValue("Description"),
                HouseRules = reader.GetStringValue("HouseRules"),
                CurrencyLayout = reader.GetStringValue("CurrencyLayout"),
                CurrencySymbol = reader.GetStringValue("Currency"),
                TimezoneName = reader.GetStringValue("Timezone"),
                DefaultBuyin = reader.GetIntValue("DefaultBuyin"),
                CashgamesEnabled = reader.GetBooleanValue("CashgamesEnabled"),
                TournamentsEnabled = reader.GetBooleanValue("TournamentsEnabled"),
                VideosEnabled = reader.GetBooleanValue("VideosEnabled")
            };
        }

        public static RawBunch Create(Bunch bunch)
        {
            return new RawBunch
            {
                Id = bunch.Id,
                Slug = bunch.Slug,
                DisplayName = bunch.DisplayName,
                Description = bunch.Description,
                HouseRules = bunch.HouseRules,
                CurrencyLayout = bunch.Currency.Layout,
                CurrencySymbol = bunch.Currency.Symbol,
                TimezoneName = bunch.Timezone.Id,
                DefaultBuyin = bunch.DefaultBuyin,
                CashgamesEnabled = bunch.CashgamesEnabled,
                TournamentsEnabled = bunch.TournamentsEnabled,
                VideosEnabled = bunch.VideosEnabled
            };
        }
    }
}
