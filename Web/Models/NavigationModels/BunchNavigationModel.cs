using Core.Urls;
using Core.UseCases.BunchContext;

namespace Web.Models.NavigationModels
{
    public class BunchNavigationModel
    {
        public string Heading { get; private set; }
        public Url HeadingUrl { get; private set; }
        public Url CashgameUrl { get; private set; }
        public Url PlayerUrl { get; private set; }
        public Url EventUrl { get; private set; }
        public bool IsEmpty { get; private set; }

        protected BunchNavigationModel()
        {
            IsEmpty = false;
        }

        public BunchNavigationModel(BunchContextResult bunchContextResult)
            : this()
        {
            Heading = bunchContextResult.BunchName;
            HeadingUrl = new BunchDetailsUrl(bunchContextResult.Slug);
            CashgameUrl = new CashgameIndexUrl(bunchContextResult.Slug);
            PlayerUrl = new PlayerIndexUrl(bunchContextResult.Slug);
            EventUrl = new EventListUrl(bunchContextResult.Slug);
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
                Heading = "";
                HeadingUrl = new EmptyUrl();
                CashgameUrl = new EmptyUrl();
                PlayerUrl = new EmptyUrl();
                EventUrl = new EmptyUrl();
            }
        }
    }
}
