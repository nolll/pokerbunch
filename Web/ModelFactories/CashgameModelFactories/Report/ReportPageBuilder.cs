using Application.UseCases.BunchContext;
using Web.Models.CashgameModels.Report;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.Report
{
    public class ReportPageBuilder : IReportPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;

        public ReportPageBuilder(
            IBunchContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public ReportPageModel Build(string slug, ReportPostModel postModel)
        {
            var model = Build(slug);
            if (postModel != null)
            {
                model.StackAmount = postModel.StackAmount;
            }
            return model;
        }

        private ReportPageModel Build(string slug)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest{Slug = slug});

            return new ReportPageModel
                {
                    BrowserTitle = "Report Stack",
                    PageProperties = new PageProperties(contextResult),
                };
        }
    }
}