using Core.Entities;
using Core.Repositories;
using Web.ModelFactories.PageBaseModelFactories;
using Web.Models.CashgameModels.Cashout;

namespace Web.ModelFactories.CashgameModelFactories.Cashout
{
    public class CashoutPageBuilder : ICashoutPageBuilder
    {
        private readonly IPagePropertiesFactory _pagePropertiesFactory;
        private readonly IHomegameRepository _homegameRepository;

        public CashoutPageBuilder(
            IPagePropertiesFactory pagePropertiesFactory,
            IHomegameRepository homegameRepository)
        {
            _pagePropertiesFactory = pagePropertiesFactory;
            _homegameRepository = homegameRepository;
        }

        public CashoutPageModel Build(string slug, CashoutPostModel postModel)
        {
            var homegame = _homegameRepository.GetBySlug(slug);
            
            var model = Build(homegame);
            if (postModel != null)
            {
                model.StackAmount = postModel.StackAmount;                
            }
            return model;
        }

        private CashoutPageModel Build(Homegame homegame)
        {
            return new CashoutPageModel
                {
                    BrowserTitle = "Cash Out",
                    PageProperties = _pagePropertiesFactory.Create(homegame)
                };
        }
    }
}