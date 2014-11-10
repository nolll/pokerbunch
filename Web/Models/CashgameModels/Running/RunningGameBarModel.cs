namespace Web.Models.CashgameModels.Running
{
    public class RunningGameBarModel : BarModel
    {
        public RunningGameBarModel(string url) : base(url)
        {
        }

        public override bool GameIsRunning
        {
            get { return true; }
        }
    }
}