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

        public EditCheckpointModel Create(User user, Homegame homegame, Checkpoint checkpoint, string dateStr, string playerName)
        {
            return new EditCheckpointModel
                {
                    BrowserTitle = "Edit Checkpoint",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
                    Timestamp = TimeZoneInfo.ConvertTime(checkpoint.Timestamp, homegame.Timezone),
                    Stack = checkpoint.Stack,
                    Amount = checkpoint.Amount,
                    DeleteUrl = _urlProvider.GetCashgameCheckpointDeleteUrl(homegame.Slug, dateStr, playerName, checkpoint.Id),
                    CancelUrl = _urlProvider.GetCashgameActionUrl(homegame.Slug, dateStr, playerName),
                    EnableAmountField = checkpoint.Type == CheckpointType.Buyin,
                    StackLabel = checkpoint.Type == CheckpointType.Buyin ? "Stack after buyin" : "Stack"
                };
        }
    }
}