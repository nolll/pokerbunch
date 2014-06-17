using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Report;

namespace Web.ModelFactories.CashgameModelFactories.Report
{
    public class ReportPageBuilder : IReportPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public ReportPageBuilder(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        private ReportPageModel Create(Homegame homegame)
        {
            return new ReportPageModel
                {
                    BrowserTitle = "Report Stack",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                };
        }

        public ReportPageModel Build(Homegame homegame, ReportPostModel postModel)
        {
            var model = Create(homegame);
            if (postModel != null)
            {
                model.StackAmount = postModel.StackAmount;
            }
            return model;
        }
    }
}