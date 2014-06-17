using Web.Models.CashgameModels.Report;

namespace Web.ModelFactories.CashgameModelFactories.Report
{
    public interface IReportPageBuilder
    {
        ReportPageModel Build(string slug, ReportPostModel postModel = null);
    }
}