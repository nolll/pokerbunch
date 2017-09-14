using Core.UseCases;
using Web.Extensions;
using Web.Urls.SiteUrls;

namespace Web.Models.NavigationModels
{
    public class BunchNavigationModel : IViewModel
    {
        public string Heading { get; private set; }
        public string HeadingUrl { get; private set; }
        public string CashgameUrl { get; private set; }
        public string PlayerUrl { get; private set; }
        public string EventUrl { get; private set; }
        public string LocationUrl { get; private set; }
        public bool IsEmpty { get; private set; }

        protected BunchNavigationModel()
        {
            IsEmpty = false;
        }

        public BunchNavigationModel(BunchContext.Result bunchContextResult)
            : this()
        {
            Heading = bunchContextResult.BunchName;
            HeadingUrl = new BunchDetailsUrl(bunchContextResult.Slug).Relative;
            CashgameUrl = new CashgameIndexUrl(bunchContextResult.Slug).Relative;
            PlayerUrl = new PlayerIndexUrl(bunchContextResult.Slug).Relative;
            EventUrl = new EventListUrl(bunchContextResult.Slug).Relative;
            LocationUrl = new LocationListUrl(bunchContextResult.Slug).Relative;
        }

        public static BunchNavigationModel Empty => new EmptyBunchNavigationModel();

        private class EmptyBunchNavigationModel : BunchNavigationModel
        {
            public EmptyBunchNavigationModel()
            {
                IsEmpty = true;
            }
        }

        public View GetView()
        {
            return new View("~/Views/Navigation/BunchNavigation.cshtml");
        }
    }
}
