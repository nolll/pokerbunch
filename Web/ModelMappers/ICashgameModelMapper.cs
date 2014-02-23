using Core.Classes;
using Web.Models.CashgameModels.Edit;

namespace Web.ModelMappers
{
    public interface ICashgameModelMapper
    {
        Cashgame Map(Cashgame cashgame, CashgameEditPostModel postModel);
    }
}