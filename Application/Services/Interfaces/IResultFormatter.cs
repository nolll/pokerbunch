namespace Application.Services.Interfaces
{
    public interface IResultFormatter
    {
        string FormatWinnings(int winnings);
        string GetWinningsCssClass(int winnings);
    }
}