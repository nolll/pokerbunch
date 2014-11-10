namespace Web.Models.CashgameModels.Running
{
    public class BarModel
    {
        public string Url { get; private set; }

        public BarModel(string url)
        {
            Url = url;
        }

        public virtual bool GameIsRunning
        {
            get { return false; }
        }
    }
}