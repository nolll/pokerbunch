using Core.UseCases;
using Web.Annotations;

namespace Web.Models.CashgameModels.Running
{
    public class BunchPlayerJsonModel
    {
        [UsedImplicitly]
        public int Id { get; private set; }

        [UsedImplicitly]
        public string Name { get; private set; }

        [UsedImplicitly]
        public string Color { get; private set; }

        public BunchPlayerJsonModel(RunningCashgame.BunchPlayerItem item)
        {
            Id = item.PlayerId;
            Name = item.Name;
            Color = item.Color;
        }
    }
}