using Core.Urls;
using Core.UseCases;
using Web.Urls;

namespace Web.Models.NavigationModels
{
    public class BunchNavigationModel
    {
        public string Heading { get; private set; }
        public string HeadingUrl { get; private set; }
        public string CashgameUrl { get; private set; }
        public string PlayerUrl { get; private set; }
        public string EventUrl { get; private set; }
        public bool IsEmpty { get; private set; }

        protected BunchNavigationModel()
        {
            IsEmpty = false;
        }

        public BunchNavigationModel(BunchContext.Result bunchContextResult)
            : this()
        {
            Heading = bunchContextResult.BunchName;
            HeadingUrl = bunchContextResult.BunchUrl.Relative;
            CashgameUrl = bunchContextResult.CashgameUrl.Relative;
            PlayerUrl = new PlayerIndexUrl(bunchContextResult.Slug).Relative;
            EventUrl = bunchContextResult.EventUrl.Relative;
        }

        public static BunchNavigationModel Empty
        {
            get
            {
                return new EmptyBunchNavigationModel();
            }
        }

        private class EmptyBunchNavigationModel : BunchNavigationModel
        {
            public EmptyBunchNavigationModel()
            {
                IsEmpty = true;
            }
        }
    }
}
