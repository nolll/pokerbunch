namespace Core.UseCases.CashgameChart
{
    public class ChartPlayerItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }

        public ChartPlayerItem(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}