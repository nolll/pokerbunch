using Application.Urls;

namespace Web.Models.CashgameModels.Running
{
    public class RunningGameBarModel : BarModel
    {
        public RunningGameBarModel(string slug) : base(slug)
        {
        }

        public override bool GameIsRunning
        {
            get { return true; }
        }

        public override Url Url
        {
            get { return new RunningCashgameUrl(Slug); }
        }
    }
}