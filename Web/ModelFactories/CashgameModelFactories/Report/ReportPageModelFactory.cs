using Core.Classes;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Report;

namespace Web.ModelFactories.CashgameModelFactories.Report
{
    public class ReportPageModelFactory : IReportPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public ReportPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        private ReportPageModel Create(User user, Homegame homegame)
        {
            return new ReportPageModel
                {
                    BrowserTitle = "Report Stack",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame),
                };
        }

        public ReportPageModel Create(User user, Homegame homegame, ReportPostModel postModel)
        {
            var model = Create(user, homegame);
            if (postModel != null)
            {
                model.StackAmount = postModel.StackAmount;
            }
            return model;
        }
    }
}