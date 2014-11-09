using System;
using System.Collections.Generic;

namespace Core.UseCases.Actions
{
    public class ActionsOutput
    {
        public DateTime Date { get; private set; }
        public string PlayerName { get; private set; }
        public IList<CheckpointItem> CheckpointItems { get; private set; }
        
        public ActionsOutput(DateTime date, string playerName, List<CheckpointItem> checkpointItems)
        {
            Date = date;
            PlayerName = playerName;
            CheckpointItems = checkpointItems;
        }
    }
}