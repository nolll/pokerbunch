using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Report;

namespace Web.ModelFactories.CashgameModelFactories.Report
{
    public class ReportPageBuilder : IReportPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;

        public ReportPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
        }

        public ReportPageModel Build(string slug, ReportPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            
            var model = Build(homegame);
            if (postModel != null)
            {
                model.StackAmount = postModel.StackAmount;
            }
            return model;
        }

        private ReportPageModel Build(Homegame homegame)
        {
            return new ReportPageModel
                {
                    BrowserTitle = "Report Stack",
                    PageProperties = _pagePropertiesFactory.Create(homegame),
                };
        }
    }
}