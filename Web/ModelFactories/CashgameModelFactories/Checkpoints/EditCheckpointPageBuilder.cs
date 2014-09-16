using Application.UseCases.BunchContext;
using Application.UseCases.EditCheckpointForm;
using Plumbing;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public class EditCheckpointPageBuilder : IEditCheckpointPageBuilder
    {
        public EditCheckpointPageModel Build(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel)
        {
            var contextResult = DependencyContainer.Instance.BunchContext(new BunchContextRequest(slug));
            var editCheckpointFormResult = DependencyContainer.Instance.EditCheckpointForm(new EditCheckpointFormRequest(slug, dateStr, playerId, checkpointId));

            return new EditCheckpointPageModel(contextResult, editCheckpointFormResult, postModel);
        }
    }
}