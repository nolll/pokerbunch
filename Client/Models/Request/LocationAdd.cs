namespace PokerBunch.Client.Models.Request
{
    public class LocationAdd
    {
        public string Name { get; }

        public LocationAdd(string name)
        {
            Name = name;
        }
    }
}