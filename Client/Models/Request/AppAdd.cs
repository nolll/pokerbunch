namespace PokerBunch.Client.Models.Request
{
    public class AppAdd
    {
        public string Name { get; }

        public AppAdd(string name)
        {
            Name = name;
        }
    }
}