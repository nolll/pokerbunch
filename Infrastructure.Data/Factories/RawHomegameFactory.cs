using Core.Entities;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Interfaces;

namespace Infrastructure.Data.Factories
{
    public static class RawHomegameFactory
    {
        public static RawHomegame Create(IStorageDataReader reader)
        {
            return new RawHomegame
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

        public static RawHomegame Create(Homegame homegame)
        {
            return new RawHomegame
            {
                Id = homegame.Id,
                Slug = homegame.Slug,
                DisplayName = homegame.DisplayName,
                Description = homegame.Description,
                HouseRules = homegame.HouseRules,
                CurrencyLayout = homegame.Currency.Layout,
                CurrencySymbol = homegame.Currency.Symbol,
                TimezoneName = homegame.Timezone.Id,
                DefaultBuyin = homegame.DefaultBuyin,
                CashgamesEnabled = homegame.CashgamesEnabled,
                TournamentsEnabled = homegame.TournamentsEnabled,
                VideosEnabled = homegame.VideosEnabled
            };
        }
    }
}
