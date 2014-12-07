using Core.UseCases.RunningCashgame;
using Web.Annotations;

namespace Web.Models.CashgameModels.Running
{
    public class BunchPlayerJsonModel
    {
        [UsedImplicitly]
        public int Id { get; private set; }
        
        [UsedImplicitly]
        public string Name { get; private set; }

        public BunchPlayerJsonModel(BunchPlayerItem item)
        {
            Id = item.PlayerId;
            Name = item.Name;
        }
    }
}