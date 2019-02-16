namespace PokerBunch.Client.Models.Request
{
    public class BunchUpdate
    {
        public string Id { get; }
        public string Description { get; }
        public string HouseRules { get; }
        public string Timezone { get; }
        public string CurrencySymbol { get; }
        public string CurrencyLayout { get; }
        public int DefaultBuyin { get; }

        public BunchUpdate(string id, string description, string houseRules, string timezone, string currencySymbol, string currencyLayout, int defaultBuyin)
        {
            Id = id;
            Description = description;
            HouseRules = houseRules;
            Timezone = timezone;
            CurrencySymbol = currencySymbol;
            CurrencyLayout = currencyLayout;
            DefaultBuyin = defaultBuyin;
        }
    }
}