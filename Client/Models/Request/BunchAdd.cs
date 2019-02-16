namespace PokerBunch.Client.Models.Request
{
    public class BunchAdd
    {
        public string Name { get; }
        public string Description { get; }
        public string Timezone { get; }
        public string CurrencySymbol { get; }
        public string CurrencyLayout { get; }

        public BunchAdd(string name, string description, string timezone, string currencySymbol, string currencyLayout)
        {
            Name = name;
            Description = description;
            Timezone = timezone;
            CurrencySymbol = currencySymbol;
            CurrencyLayout = currencyLayout;
        }
    }
}