using System;
using Application.Urls;
using Core.Entities.Checkpoints;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Checkpoints;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public class EditCheckpointPageBuilder : IEditCheckpointPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;
        private readonly ICheckpointRepository _checkpointRepository;

        public EditCheckpointPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository,
            ICheckpointRepository checkpointRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
            _checkpointRepository = checkpointRepository;
        }

        public EditCheckpointPageModel Build(string slug, string dateStr, int playerId, int checkpointId)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            var checkpoint = _checkpointRepository.GetCheckpoint(checkpointId);
            
            return new EditCheckpointPageModel
                {
                    BrowserTitle = "Edit Checkpoint",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    Timestamp = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, homegame.Timezone),
                    Stack = checkpoint.Stack,
                    Amount = checkpoint.Amount,
                    DeleteUrl = new DeleteCheckpointUrl(homegame.Slug, dateStr, playerId, checkpoint.Id),
                    CancelUrl = new CashgameActionUrl(homegame.Slug, dateStr, playerId),
                    EnableAmountField = checkpoint.Type == CheckpointType.Buyin,
                    StackLabel = checkpoint.Type == CheckpointType.Buyin ? "Stack after buyin" : "Stack"
                };
        }

        public EditCheckpointPageModel Build(string slug, string dateStr, int playerId, int checkpointId, EditCheckpointPostModel postModel)
        {
            var model = Build(slug, dateStr, playerId, checkpointId);
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