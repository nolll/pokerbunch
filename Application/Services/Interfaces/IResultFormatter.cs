namespace Application.Services
{
    public interface IResultFormatter
    {
        string FormatWinnings(int winnings);
        string GetWinningsCssClass(int winnings);
    }
}