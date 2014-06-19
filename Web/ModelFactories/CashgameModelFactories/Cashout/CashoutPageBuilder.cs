using Application.UseCases.BunchContext;
using Web.Models.CashgameModels.Cashout;
using Web.Models.PageBaseModels;

namespace Web.ModelFactories.CashgameModelFactories.Cashout
{
    public class CashoutPageBuilder : ICashoutPageBuilder
    {
        private readonly IBunchContextInteractor _bunchContextInteractor;

        public CashoutPageBuilder(
            IBunchContextInteractor bunchContextInteractor)
        {
            _bunchContextInteractor = bunchContextInteractor;
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
            var contextResult = _bunchContextInteractor.Execute(new BunchContextRequest {Slug = slug});

            return new CashoutPageModel
                {
                    BrowserTitle = "Cash Out",
                    PageProperties = new PageProperties(contextResult)
                };
        }
    }
}