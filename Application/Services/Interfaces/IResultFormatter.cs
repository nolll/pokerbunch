using Application.UseCases.CashgameFacts;

namespace Application.Services
{
    public interface IResultFormatter
    {
        string FormatWinnings(int winnings);
        string GetWinningsCssClass(Money winnings);
        string GetWinningsCssClass(int winnings);
    }
}