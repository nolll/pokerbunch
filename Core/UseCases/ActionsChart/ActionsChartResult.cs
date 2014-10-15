using System.Collections.Generic;

namespace Core.UseCases.ActionsChart
{
    public class ActionsChartResult
    {
        public IList<ActionsChartCheckpointItem> CheckpointItems { get; private set; }

        public ActionsChartResult(IList<ActionsChartCheckpointItem> checkpointItems)
        {
            CheckpointItems = checkpointItems;
        }
    }
}