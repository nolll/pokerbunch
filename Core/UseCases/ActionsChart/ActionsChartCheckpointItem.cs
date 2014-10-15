using System;

namespace Core.UseCases.ActionsChart
{
    public class ActionsChartCheckpointItem
    {
        public DateTime Timestamp { get; private set; }
        public int Stack { get; private set; }
        public int TotalBuyin { get; private set; }
        public int AddedMoney { get; private set; }

        public ActionsChartCheckpointItem(DateTime timestamp, int stack, int totalBuyin, int addedMoney = 0)
        {
            Timestamp = timestamp;
            Stack = stack;
            TotalBuyin = totalBuyin;
            AddedMoney = addedMoney;
        }
    }
}