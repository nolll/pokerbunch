using Core.Entities;
using Web.Models.CashgameModels.Report;

namespace Web.ModelFactories.CashgameModelFactories.Report
{
    public interface IReportPageModelFactory
    {
        ReportPageModel Create(Homegame homegame, ReportPostModel postModel);
    }
}