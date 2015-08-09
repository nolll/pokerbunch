using Core.UseCases;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class BunchPageModel : AppPageModel
    {
        public BunchNavigationModel BunchNavModel { get; private set; }

        protected BunchPageModel(string browserTitle, BunchContext.Result bunchContextResult)
            : base(browserTitle, bunchContextResult.AppContext)
        {
            BunchNavModel = GetBunchNavModel(bunchContextResult);
        }

        public override string Layout
        {
            get { return ContextLayout.Bunch; }
        }

        private BunchNavigationModel GetBunchNavModel(BunchContext.Result bunchContextResult)
        {
            if (bunchContextResult.HasBunch)
                return new BunchNavigationModel(bunchContextResult);
            return BunchNavigationModel.Empty;
        }
    }
}