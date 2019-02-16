namespace PokerBunch.Client.Models.Response
{
    public class Bunch : BunchSmall
    {
        public string HouseRules { get; set; }
        public string Timezone { get; set; }
        public string CurrencySymbol { get; set; }
        public string CurrencyLayout { get; set; }
        public int DefaultBuyin { get; set; }
        public BunchPlayer Player { get; set; }
        public string Role { get; set; }
    }
}