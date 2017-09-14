using System.Collections.Generic;
using System.Linq;
using Core.UseCases;
using Web.Extensions;

namespace Web.Models.CashgameModels.Action
{
    public class CheckpointListModel : List<CheckpointModel>, IViewModel
    {
        public CheckpointListModel(Actions.Result actionsResult)
        {
            AddRange(actionsResult.CheckpointItems.Select(o => new CheckpointModel(o)).ToList());
        }

        public View GetView()
        {
            return new View("CashgameAction/CheckpointList");
        }
    }
}