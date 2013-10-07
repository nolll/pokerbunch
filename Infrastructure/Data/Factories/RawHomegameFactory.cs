using Core.Classes;
using Infrastructure.Data.Classes;
using Infrastructure.Data.Storage;

namespace Infrastructure.Data.Factories
{
    internal class RawHomegameFactory : IRawHomegameFactory
    {
        public RawHomegame Create(StorageDataReader reader)
        {
            return new RawHomegame
            {
                Id = reader.GetInt("HomegameID"),
                Slug = reader.GetString("Name"),
                DisplayName = reader.GetString("DisplayName"),
                Description = reader.GetString("Description"),
                HouseRules = reader.GetString("HouseRules"),
                CurrencyLayout = reader.GetString("CurrencyLayout"),
                CurrencySymbol = reader.GetString("Currency"),
                TimezoneName = reader.GetString("Timezone"),
                DefaultBuyin = reader.GetInt("DefaultBuyin"),
                CashgamesEnabled = reader.GetBoolean("CashgamesEnabled"),
                TournamentsEnabled = reader.GetBoolean("TournamentsEnabled"),
                VideosEnabled = reader.GetBoolean("VideosEnabled")
            };
        }

        public RawHomegame Create(Homegame homegame)
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
