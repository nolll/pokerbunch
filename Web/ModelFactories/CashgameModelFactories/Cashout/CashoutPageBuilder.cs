using Application.UseCases.BunchContext;
using Web.Models.CashgameModels.Cashout;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.Cashout
{
    public class CashoutPageBuilder : ICashoutPageBuilder
    {
        private readonly IBunchContextInteractor _contextInteractor;

        public CashoutPageBuilder(
            IBunchContextInteractor contextInteractor)
        {
            _contextInteractor = contextInteractor;
        }

        public CashoutPageModel Build(string slug, CashoutPostModel postModel)
        {
            var model = Build(slug);
            if (postModel != null)
            {
                model.StackAmount = postModel.StackAmount;                
            }
            return model;
        }

        private CashoutPageModel Build(string slug)
        {
            var contextResult = _contextInteractor.Execute(new BunchContextRequest {Slug = slug});

            return new CashoutPageModel
                {
                    BrowserTitle = "Cash Out",
                    PageProperties = new PageProperties(contextResult)
                };
        }
    }
}