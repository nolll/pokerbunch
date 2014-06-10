using System;
using Application.Services;
using Core.Entities;
using Core.Entities.Checkpoints;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Checkpoints;
using Web.Models.UrlModels;
using Web.Services;

namespace Web.ModelFactories.CashgameModelFactories.Checkpoints
{
    public class EditCheckpointPageModelFactory : IEditCheckpointPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IUrlProvider _urlProvider;

        public EditCheckpointPageModelFactory(
            IPagePropertiesFactory pagePropertiesFactory,
            IUrlProvider urlProvider)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _urlProvider = urlProvider;
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
                    DeleteUrl = new DeleteCheckpointUrlModel(homegame.Slug, dateStr, playerId, checkpoint.Id),
                    CancelUrl = new CashgameActionUrlModel(homegame.Slug, dateStr, playerId),
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