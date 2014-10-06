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
        public bool IsEmpty { get; private set; }

        protected BunchNavigationModel()
            : this("", "")
        {
        }

        public BunchNavigationModel(BunchContextResult bunchContextResult)
            : this(bunchContextResult.Slug, bunchContextResult.BunchName)
        {
        }

        private BunchNavigationModel(string slug, string bunchName)
        {
            Heading = bunchName;
            HeadingUrl = new BunchDetailsUrl(slug);
            CashgameUrl = new CashgameIndexUrl(slug);
            PlayerUrl = new PlayerIndexUrl(slug);
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
