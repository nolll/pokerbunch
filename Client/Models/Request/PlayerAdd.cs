namespace PokerBunch.Client.Models.Request
{
    public class PlayerAdd
    {
        public string Name { get; }

        public PlayerAdd(string name)
        {
            Name = name;
        }
    }
}