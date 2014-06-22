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
            var contextResult = _contextInteractor.Execute(new BunchContextRequest(slug));

            var model = new ReportPageModel(contextResult);
            
            if (postModel != null)
            {
                model.StackAmount = postModel.StackAmount;
            }
            
            return model;
        }
    }
}