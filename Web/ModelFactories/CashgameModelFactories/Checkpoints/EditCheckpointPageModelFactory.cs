using System;
using Core.Entities;
using Core.Entities.Checkpoints;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.UrlModels;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public class EditCheckpointPageModelFactory : IEditCheckpointPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public EditCheckpointPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        public EditCheckpointPageModel Create(Homegame homegame, Checkpoint checkpoint, string dateStr, int playerId)
        {
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

        public EditCheckpointPageModel Create(Homegame homegame, Checkpoint checkpoint, string dateStr, int playerId, EditCheckpointPostModel postModel)
        {
            var model = Create(homegame, checkpoint, dateStr, playerId);
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