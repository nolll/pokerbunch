using Core.Urls;

namespace Web.Models.CashgameModels.Running
{
    public class BarModel
    {
        protected readonly string Slug;

        public BarModel(string slug)
        {
            Slug = slug;
        }

        public virtual bool GameIsRunning
        {
            get { return false; }
        }

        public virtual Url Url
        {
            get { return new AddCashgameUrl(Slug); }
        }
    }
}