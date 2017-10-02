using JetBrains.Annotations;

namespace Infrastructure.Api.Models
{
    internal class ApiEventCashgame
    {
        [UsedImplicitly]
        public string CashgameId { get; set; }

        public ApiEventCashgame(string cashgameId)
        {
            CashgameId = cashgameId;
        }
    }
}