using Core.Entities;
using Web.Models.CashgameModels.Report;

namespace Web.ModelFactories.CashgameModelFactories.Report
{
    public interface IReportPageBuilder
    {
        ReportPageModel Build(Homegame homegame, ReportPostModel postModel);
    }
}