using Web.Models.CashgameModels.Add;
using Web.Models.CashgameModels.Edit;

namespace Web.Commands.CashgameCommands
{
    public interface ICashgameCommandProvider
    {
        Command GetEndGameCommand(string slug);
        Command GetAddCommand(string slug, AddCashgamePostModel postModel);
        Command GetEditCommand(string slug, string dateStr, CashgameEditPostModel postModel);
    }
}