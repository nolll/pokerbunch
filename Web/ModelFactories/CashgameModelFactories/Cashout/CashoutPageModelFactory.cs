using Core.Classes;
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

        public CashoutPageModel Create(User user, Homegame homegame)
        {
            return new CashoutPageModel
                {
                    BrowserTitle = "Cash Out",
                    PageProperties = _pagePropertiesFactory.Create(user, homegame)
                };
        }

        public CashoutPageModel Create(User user, Homegame homegame, CashoutPostModel postModel)
        {
            var model = Create(user, homegame);
            model.StackAmount = postModel.StackAmount;
            return model;
        }
    }
}