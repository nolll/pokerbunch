using Core.Classes;
using Web.Models.CashgameModels.Report;

namespace Web.ModelFactories.CashgameModelFactories.Report
{
    public interface IReportPageModelFactory
    {
        ReportPageModel Create(User user, Homegame homegame, ReportPostModel postModel);
    }
}