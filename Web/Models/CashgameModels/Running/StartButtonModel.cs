using Core.Urls;

namespace Web.Models.CashgameModels.Running
{
    public class StartButtonModel
    {
        public Url Url { get; set; }

        public StartButtonModel(string slug)
        {
            Url = new AddCashgameUrl(slug);
        }

        public virtual bool GameIsRunning
        {
            get { return false; }
        }
    }

    class RunningGameStartButtonModel : StartButtonModel
    {
        public RunningGameStartButtonModel(string slug) : base(slug)
        {
        }

        public override bool GameIsRunning
        {
            get { return true; }
        }
    }
}