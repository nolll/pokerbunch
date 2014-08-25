using System;
using Application.Urls;
using Application.UseCases.BunchContext;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public class EditCheckpointPageBuilder : IEditCheckpointPageBuilder
    {
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICheckpointRepository _checkpointRepository;
        private readonly IBunchContextInteractor _contextInteractor;

        public EditCheckpointPageBuilder(
            IHomegameRepository homegameRepository,
            ICheckpointRepository checkpointRepository,
            IBunchContextInteractor contextInteractor)
        {
            _homegameRepository = homegameRepository;
            _checkpointRepository = checkpointRepository;
            _contextInteractor = contextInteractor;
        }

        public EditCheckpointPageModel Build(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var checkpoint = _checkpointRepository.GetCheckpoint(checkpointId);

            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            var model = new EditCheckpointPageModel(contextResult)
            {
                Timestamp = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, homegame.Timezone),
                Stack = checkpoint.Stack,
                Amount = checkpoint.Amount,
                DeleteUrl = new DeleteCheckpointUrl(homegame.Slug, dateStr, playerId, checkpoint.Id),
                CancelUrl = new CashgameActionUrl(homegame.Slug, dateStr, playerId),
                EnableAmountField = checkpoint.Type == CheckpointType.Buyin,
                StackLabel = checkpoint.Type == CheckpointType.Buyin ? "Stack after buyin" : "Stack"
            };

            if (postModel != null)
            {
                model.Timestamp = postModel.Timestamp;
                model.Stack = postModel.Stack;
                model.Amount = postModel.Amount;
            }
            
            return model;
        }
    }
}