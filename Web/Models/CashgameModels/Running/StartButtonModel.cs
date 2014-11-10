namespace Web.Models.CashgameModels.Running
{
    public class StartButtonModel
    {
        public string NewUrl { get; private set; }

        public StartButtonModel(string newUrl)
        {
            NewUrl = newUrl;
        }

        public virtual bool GameIsRunning
        {
            get { return false; }
        }
    }

    class RunningGameStartButtonModel : StartButtonModel
    {
        public RunningGameStartButtonModel(string newUrl) : base(newUrl)
        {
        }

        public override bool GameIsRunning
        {
            get { return true; }
        }
    }
}