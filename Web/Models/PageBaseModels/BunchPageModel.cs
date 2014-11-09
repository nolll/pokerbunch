using Core.UseCases.BunchContext;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class BunchPageModel : AppPageModel
    {
        public BunchNavigationModel BunchNavModel { get; private set; }

        protected BunchPageModel(string browserTitle, BunchContextResult bunchContextResult)
            : base(browserTitle, bunchContextResult.AppContext)
        {
            BunchNavModel = GetBunchNavModel(bunchContextResult);
        }

        public override string Layout
        {
            get { return ContextLayout.Bunch; }
        }

        private BunchNavigationModel GetBunchNavModel(BunchContextResult bunchContextResult)
        {
            if (bunchContextResult.HasBunch)
                return new BunchNavigationModel(bunchContextResult);
            return BunchNavigationModel.Empty;
        }
    }
}