using System;
using Application.Services;
using Core.Classes;
using Core.Classes.Checkpoints;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Checkpoints;

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

        public EditCheckpointPageModel Create(Homegame homegame, Checkpoint checkpoint, string dateStr, string playerName)
        {
            return new EditCheckpointPageModel
                {
                    BrowserTitle = "Edit Checkpoint",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                    Timestamp = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, homegame.Timezone),
                    Stack = checkpoint.Stack,
                    Amount = checkpoint.Amount,
                    DeleteUrl = _urlProvider.GetCashgameCheckpointDeleteUrl(homegame.Slug, dateStr, playerName, checkpoint.Id),
                    CancelUrl = _urlProvider.GetCashgameActionUrl(homegame.Slug, dateStr, playerName),
                    EnableAmountField = checkpoint.Type == CheckpointType.Buyin,
                    StackLabel = checkpoint.Type == CheckpointType.Buyin ? "Stack after buyin" : "Stack"
                };
        }

        public EditCheckpointPageModel Create(Homegame homegame, Checkpoint checkpoint, string dateStr, string playerName, EditCheckpointPostModel postModel)
        {
            var model = Create(homegame, checkpoint, dateStr, playerName);
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