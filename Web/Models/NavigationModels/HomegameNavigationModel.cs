using Application.Urls;
using Application.UseCases.BunchContext;

namespace Web.Models.NavigationModels
{
    public class HomegameNavigationModel
    {
        public string Heading { get; private set; }
        public Url HeadingUrl { get; private set; }
        public Url CashgameUrl { get; private set; }
        public Url PlayerUrl { get; private set; }
        public bool IsEmpty { get; private set; }

        protected HomegameNavigationModel()
            : this("", "")
        {
        }

        public HomegameNavigationModel(BunchContextResult bunchContextResult)
            : this(bunchContextResult.Slug, bunchContextResult.BunchName)
        {
        }

        private HomegameNavigationModel(string slug, string bunchName)
        {
            Heading = bunchName;
            HeadingUrl = new HomegameDetailsUrl(slug);
            CashgameUrl = new CashgameIndexUrl(slug);
            PlayerUrl = new PlayerIndexUrl(slug);
        }

        public static HomegameNavigationModel Empty
        {
            get
            {
                return new EmptyHomegameNavigationModel();
            }
        }

        private class EmptyHomegameNavigationModel : HomegameNavigationModel
        {
            public EmptyHomegameNavigationModel()
            {
                IsEmpty = true;
            }
        }
    }
}
