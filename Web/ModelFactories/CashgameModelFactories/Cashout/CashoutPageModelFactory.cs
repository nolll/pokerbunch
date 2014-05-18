using Core.Entities;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Cashout;

namespace Web.ModelFactories.CashgameModelFactories.Cashout
{
    public class CashoutPageModelFactory : ICashoutPageModelFactory
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;

        public CashoutPageModelFactory(IPagePropertiesFactory pagePropertiesFactory)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
        }

        private CashoutPageModel Create(Homegame homegame)
        {
            return new CashoutPageModel
                {
                    BrowserTitle = "Cash Out",
                    PageProperties = _pagePropertiesFactory.Create(homegame)
                };
        }

        public CashoutPageModel Create(Homegame homegame, CashoutPostModel postModel)
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