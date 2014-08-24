using Application.UseCases.BunchContext;
using Web.Models.NavigationModels;

namespace Web.Models.PageBaseModels
{
    public abstract class BunchPageModel : AppPageModel
    {
        public HomegameNavigationModel HomegameNavModel { get; private set; }

        protected BunchPageModel(string browserTitle, BunchContextResult bunchContextResult)
            : base(browserTitle, bunchContextResult.AppContext)
        {
            HomegameNavModel = GetHomegameNavModel(bunchContextResult);
        }

        public override string Layout
        {
            get { return "BunchPage.cshtml"; }
        }

        private HomegameNavigationModel GetHomegameNavModel(BunchContextResult bunchContextResult)
        {
            if (bunchContextResult != null && bunchContextResult.HasBunch)
                return new HomegameNavigationModel(bunchContextResult);
            return null;
        }
    }
}