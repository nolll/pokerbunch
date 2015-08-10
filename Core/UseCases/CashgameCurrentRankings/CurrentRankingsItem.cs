using Core.Entities;

namespace Core.UseCases.CashgameCurrentRankings
{
    public class CurrentRankingsItem
    {
        public int Rank { get; private set; }
        public int PlayerId { get; private set; }
        public string Name { get; private set; }
        public Money TotalWinnings { get; private set; }
        public Money LastGameWinnings { get; private set; }

        public CurrentRankingsItem(CashgameTotalResult totalResult, CashgameResult lastGameResult, int index, Currency currency)
        {
            Rank = index + 1;
            PlayerId = totalResult.Player.Id;
            Name = totalResult.Player.DisplayName;
            TotalWinnings = new MoneyResult(totalResult.Winnings, currency);
            LastGameWinnings = lastGameResult != null ? new MoneyResult(lastGameResult.Winnings, currency) : null;
        }
    }
}