using System.Collections.Generic;

namespace Core.UseCases.CashgameDetailsChart
{
    public class DetailsChartPlayerItem
    {
        public int Id { get; private set; }
        public string Name { get; private set; }
        public IList<DetailsChartResultItem> Results { get; private set; }

        public DetailsChartPlayerItem(int id, string name, IList<DetailsChartResultItem> results)
        {
            Id = id;
            Name = name;
            Results = results;
        }
    }
}